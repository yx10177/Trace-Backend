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
        public async Task<IActionResult> Register([FromBody] LoginArgs request)
        {
            var result = await _userCenter.Register(request);
            if (result.StatusCode == EnumStatusCode.Success) 
            {
                return CreatedAtAction(nameof(GetUserData),result.Entries);
            }
            return BadRequest(result.Message.ToString());
            
        }
        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginArgs request)
        {
            var result = await _userCenter.Login(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return Ok(result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("User")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<IActionResult> GetUserData([FromQuery] RequestBase request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _userCenter.GetUserData(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return Ok(result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 更新會員資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("User")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<IActionResult> UpdateUserInfo([FromForm] UpdateUserDataArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _userCenter.UpdateUserInfo(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetUserData), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }

        /// <summary>
        /// 取得好友名單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Friend")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<IActionResult> GetUserFriends([FromQuery] RequestBase request) 
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _userCenter.GetUserFriends(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return Ok(result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }

        /// <summary>
        /// 新增好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Friend")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<IActionResult> UpdateUserFriend([FromBody] UpdateUserFriendArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _userCenter.UpdateUserFriend(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetUserFriends), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 刪除好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Friend")]
        [HttpDelete]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteUserFriends([FromBody] DeleteFriendsArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _userCenter.DeleteUserFriends(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetUserFriends), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
    }
}
