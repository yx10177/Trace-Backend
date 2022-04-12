using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trace.Model;
using Trace.Repository;
using Utils;

namespace Trace.BLL
{
    public class UserCenter : IUserCenter
    {
        private readonly IUserRepository _userRepositoy;
        private readonly IUserFriendRepository _userFriendRepository;
        public UserCenter(IUserRepository userRepository, IUserFriendRepository userFriendRepository)
        {
            _userRepositoy = userRepository;
            _userFriendRepository = userFriendRepository;
        }

        public async Task<ResponseBase<Base>> DeleteUserFriends(DeleteFriendsArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base()
            };

            try
            {
                var friends = _userFriendRepository.Get(u => u.UserId == args.Payload.UserId && args.FriendIds.Contains(u.FriendId));
                
                _userFriendRepository.DeleteRange(friends);
                await _userFriendRepository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ResponseBase<UserDataResponse>> GetUserData(RequestBase args)
        {
            var response = new ResponseBase<UserDataResponse>()
            {
                Entries = new UserDataResponse(),
            };

            try
            {
                var user = _userRepositoy.Get(u => u.UserId == args.Payload.UserId)
                                               .Select(u => new UserDataResponse
                                               {
                                                     UserId = u.UserId,
                                                     UserName = u.UserName,
                                                     UserAccount = u.UserAccount,
                                                     UserEmail = u.UserEmail,
                                                     UserPhotoPath = u.UserPhotoPath
                                                 });
                
                response.Entries = await user.SingleOrDefaultAsync();
                if (response.Entries == null)
                {
                    response.Message = "帳號不存在";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<List<UserDataResponse>>> GetUserFriends(RequestBase args)
        {
            var response = new ResponseBase<List<UserDataResponse>>()
            {
                Entries = new List<UserDataResponse>(),
            };

            try
            {
                var friends = _userFriendRepository.Get(u => u.UserId == args.Payload.UserId).Select(u => u.FriendId);
                var friendsInfo = _userRepositoy.Get(u => friends.Contains(u.UserId))
                                                .Select(u => new UserDataResponse
                                                {
                                                    UserId = u.UserId,
                                                    UserAccount = u.UserAccount,
                                                    UserName = u.UserName,
                                                    UserEmail = u.UserEmail,
                                                    UserPhotoPath = u.UserPhotoPath
                                                });
                response.Entries = await friendsInfo.ToListAsync();
                if (response.Entries == null)
                {
                    response.Message = "帳號不存在";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> Login(LoginArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {
                string encrypPwd = CrypHelper.GetMD5HashString(args.Password);
                var user = await _userRepositoy.Get(u => u.UserAccount == args.Account && u.UserPassword == encrypPwd)
                                               .Select(u => u)
                                               .SingleOrDefaultAsync();
                if (user == null) 
                {
                    response.Message = "帳號不存在";
                    return response;
                }

                response.Entries.Id = user.UserId;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> Register(LoginArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {
                if (string.IsNullOrEmpty(args.Account) || string.IsNullOrEmpty(args.Password))
                {
                    response.Message = "帳號或密碼不得為空";
                    return response;
                }
                
                if (_userRepositoy.Get(u => u.UserAccount == args.Account).Any())
                {
                    response.Message = "帳號已存在";
                    return response;
                }
                string encrypPwd = CrypHelper.GetMD5HashString(args.Password);
                var user = new User
                {
                    UserAccount = args.Account,
                    UserPassword = encrypPwd
                };
                _userRepositoy.Insert(user);
                await _userRepositoy.SaveChangesAsync();
                response.Entries.Id = user.UserId;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> UpdateUserFriend(UpdateUserFriendArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {
                var friendInfo = _userRepositoy.Get(u => u.UserAccount == args.FriendAccount).SingleOrDefault();
                if (friendInfo == null)
                {
                    response.Message = "查無此人";
                    return response;
                }
                //var userFriend= _userFriendRepository.Get(u => u.UserId == args.Payload.UserId && u.FriendId == friendInfo.UserId).SingleOrDefault();                     
                UserFriend userFriend = new UserFriend()
                {
                    UserId = args.Payload.UserId,
                    FriendId = friendInfo.UserId,
                };

                _userFriendRepository.Insert(userFriend);
                await _userFriendRepository.SaveChangesAsync();
                response.Entries.Id = friendInfo.UserId;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> UpdateUserInfo(UpdateUserDataArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {
                var user = _userRepositoy.Get(u => u.UserId == args.Payload.UserId).SingleOrDefault();
                if (user == null)
                {
                    response.Message = "無效的使用者";
                    return response;
                }
                user.UserName = args.Name;
                user.UserEmail = args.Email;
                string imagePath = $"{ConfigurationHelper.GetSection("TripPhotoDir").Value}/{DateTime.Now.ToString("yyMMddHHmmssfff")}{args.Photo.FileName}";
                using (var strearm = new FileStream(imagePath, FileMode.Create))
                {
                    args.Photo.CopyToAsync(strearm);
                }
                user.UserPhotoPath = imagePath;
                await _userRepositoy.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Task<ResponseBase<Base>> UpdateUserPwd(UpdateUserPwdArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
