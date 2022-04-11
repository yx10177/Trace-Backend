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

        [Route("TripEvent")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> SaveTripEvent([FromBody] SaveTripEventArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _tripCenter.SaveTripEvent(request);
        }

        [Route("TripEvent")]
        [HttpDelete]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> DeleteTripEvent([FromBody] Base request)
        {
            return await _tripCenter.DeleteTripEvent(request);
        }
        [Route("TripEvent")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<ResponseBase<TripEventDataResponse>> GetTripEvent([FromQuery] Base request)
        {
            return await _tripCenter.GetTripEvent(request);
        }
        /// <summary>
        /// 新增一筆旅遊紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripRecord")]
        [HttpPost]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> SaveUserRecord([FromBody] SaveUserRecordArgs request)
        {
            request.Payload = (JWTPayload)HttpContext.Items["jwtPayload"];
            return await _tripCenter.SaveUserRecord(request);
        }
        /// <summary>
        /// 刪除一筆旅遊紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripRecord")]
        [HttpDelete]
        [AuthorizationFilter]
        public async Task<ResponseBase<Base>> DeleteUserRecord([FromBody] Base request)
        {
            return await _tripCenter.DeleteUserRecord(request);
        }
        /// <summary>
        /// 取得一筆旅遊紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("TripRecord")]
        [HttpGet]
        [AuthorizationFilter]
        public async Task<ResponseBase<TripRecordDataResponse>> GetTripRecord([FromQuery] Base request)
        {
            return await _tripCenter.GetTripRecord(request);
        }
    }
}
