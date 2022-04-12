using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trace.Api.Filter;
using Trace.BLL;
using Trace.Model;

namespace Trace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripCenterController : ControllerBase
    {
        private ITripCenter _tripCenter;
        public TripCenterController(ITripCenter tripCenter)
        {
            _tripCenter = tripCenter;
        }
        /// <summary>
        /// 新增一筆旅遊事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripEvent")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<IActionResult> SaveTripEvent([FromBody] SaveTripEventArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _tripCenter.SaveTripEvent(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetTripEvent), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 刪除一筆旅遊事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripEvent")]
        [HttpDelete]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteTripEvent([FromBody] Base request)
        {
            var result = await _tripCenter.DeleteTripEvent(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetTripEvent), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 取得一筆旅遊事件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripEvent")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<IActionResult> GetTripEvent([FromQuery] Base request)
        {
            var result = await _tripCenter.GetTripEvent(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return Ok(result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 新增一筆旅遊紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripRecord")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<IActionResult> SaveUserRecord([FromBody] SaveUserRecordArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            var result = await _tripCenter.SaveUserRecord(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetTripRecord), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 刪除一筆旅遊紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripRecord")]
        [HttpDelete]
        [AuthorizationFilter]
        public async Task<IActionResult> DeleteUserRecord([FromBody] Base request)
        {
            var result = await _tripCenter.DeleteUserRecord(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return CreatedAtAction(nameof(GetTripRecord), result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
        /// <summary>
        /// 取得一筆旅遊紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripRecord")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<IActionResult> GetTripRecord([FromQuery] Base request)
        {
            var result = await _tripCenter.GetTripRecord(request);
            if (result.StatusCode == EnumStatusCode.Success)
            {
                return Ok(result.Entries);
            }
            return BadRequest(result.Message.ToString());
        }
    }
}
