using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trace.Model;

namespace Trace.BLL
{
    public interface IUserCenter
    {
        public Task<ResponseBase<Base>> Register(LoginArgs args);
        public Task<ResponseBase<Base>> Login(LoginArgs args);

        public Task<ResponseBase<Base>> UpdateUserInfo(UpdateUserDataArgs args);
        public Task<ResponseBase<Base>> UpdateUserPwd(UpdateUserPwdArgs args);
        public Task<ResponseBase<UserDataResponse>> GetUserData(RequestBase args);

        public Task<ResponseBase<Base>> UpdateUserFriend(UpdateUserFriendArgs args);
        public Task<ResponseBase<List<UserDataResponse>>> GetUserFriends(RequestBase args);
        public Task<ResponseBase<Base>> DeleteUserFriends(DeleteFriendsArgs args);
    }
}
