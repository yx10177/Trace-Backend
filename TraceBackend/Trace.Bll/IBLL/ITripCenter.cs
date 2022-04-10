using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Trace.Model;

namespace Trace.BLL
{
    public interface ITripCenter
    {
        public Task<ResponseBase<Base>> SaveTripEvent(SaveTripEventArgs args);

        public Task<ResponseBase<Base>> DeleteTripEvent(Base args);
        public Task<ResponseBase<TripEventDataResponse>> GetTripEvent(Base args);

        public Task<ResponseBase<Base>> SaveUserRecord(SaveUserRecordArgs args);
        public Task<ResponseBase<Base>> DeleteUserRecord(Base args);

        public Task<ResponseBase<TripRecordDataResponse>> GetTripRecord(Base args);

    }
}
