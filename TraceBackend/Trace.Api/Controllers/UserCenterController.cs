using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trace.Api.Filter;
using Trace.BLL;
using Trace.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Trace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCenterController : ControllerBase
    {
        private IUserCenter _userCenter;
        public UserCenterController(IUserCenter userCenter) 
        {
            _userCenter = userCenter;
        }

        /// <summary>
        /// 會員註冊
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        public async Task<ResponseBase<Base>> Register([FromBody] LoginArgs request)
        {
            return await _userCenter.Register(request);
        }
        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<ResponseBase<Base>> Login([FromBody] LoginArgs request)
        {
            return await _userCenter.Login(request);
        }
        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("User")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<ResponseBase<UserDataResponse>> GetUserData([FromBody] RequestBase request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _userCenter.GetUserData(request);
        }
        /// <summary>
        /// 更新會員資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("User")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> UpdateUserInfo([FromBody] UpdateUserDataArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _userCenter.UpdateUserInfo(request);
        }

        /// <summary>
        /// 取得好友名單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Friend")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<ResponseBase<List<UserDataResponse>>> GetUserFriends([FromBody] RequestBase request) 
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _userCenter.GetUserFriends(request);
        }

        /// <summary>
        /// 新增好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Friend")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> UpdateUserFriend([FromBody] UpdateUserFriendArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _userCenter.UpdateUserFriend(request);
        }
        /// <summary>
        /// 刪除好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Friend")]
        [HttpDelete]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> DeleteUserFriends([FromBody] DeleteFriendsArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _userCenter.DeleteUserFriends(request);
        }


    }
}
