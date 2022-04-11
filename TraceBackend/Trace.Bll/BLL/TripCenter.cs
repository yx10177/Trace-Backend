using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trace.Model;
using Trace.Repository;

namespace Trace.BLL
{
    public class TripCenter : ITripCenter
    {
        private readonly IUserRecordRepository _userRecordRepository;
        private readonly ITripEventRepository _tripEventRepositoy;
        private readonly ITripGroupRepository _tripGroupRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly ITripArticleRepository _tripArticleRepository;
        private readonly ITripPhotoRepository _tripPhotoRepository;
        public TripCenter(IUserRecordRepository userRecordRepository, ITripEventRepository tripEventRepository, ITripGroupRepository tripGroupRepository, ILocationRepository locationRepository, ITripArticleRepository tripArticleRepository, ITripPhotoRepository tripPhotoRepository)
        {
            _userRecordRepository = userRecordRepository;
            _tripEventRepositoy = tripEventRepository;
            _tripGroupRepository = tripGroupRepository;
            _locationRepository = locationRepository;
            _tripArticleRepository = tripArticleRepository;
            _tripPhotoRepository = tripPhotoRepository;
        }

        public async Task<ResponseBase<Base>> DeleteTripEvent(Base args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {

                var tripEvent = _tripEventRepositoy.Get(t => t.TripId == args.Id).SingleOrDefault();
                var tripGroup = _tripGroupRepository.Get(t => t.TripId == args.Id).ToList();
                _tripEventRepositoy.Delete(tripEvent);
                _tripGroupRepository.DeleteRange(tripGroup);

                await _tripGroupRepository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.StatusCode = EnumStatusCode.Fail;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> DeleteUserRecord(Base args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {

                var userRecord = _userRecordRepository.Get(r => r.RecordId == args.Id).SingleOrDefault();
                var articles = _tripArticleRepository.Get(r => r.RecordId == args.Id).ToList();
                var photos = _tripPhotoRepository.Get(r => r.RecordId == args.Id).ToList();
                foreach (var p in photos) 
                {
                    if (System.IO.File.Exists(p.PhotoPath))
                    {
                        System.IO.File.Delete(p.PhotoPath);
                    }
                }
                _userRecordRepository.Delete(userRecord);
                _tripArticleRepository.DeleteRange(articles);
                _tripPhotoRepository.DeleteRange(photos);

                await _userRecordRepository.SaveChangesAsync();
               

            }
            catch (Exception ex)
            {
                response.StatusCode = EnumStatusCode.Fail;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<TripEventDataResponse>> GetTripEvent(Base args)
        {
            var response = new ResponseBase<TripEventDataResponse>()
            {
                Entries = new TripEventDataResponse(),
            };

            try
            {

                var tripEvent = _tripEventRepositoy.Get(t => t.TripId == args.Id)
                                                   .Select(t => new TripEventDataResponse
                                                   {
                                                       TripId = t.TripId,
                                                       TripTitle = t.TripTitle,
                                                       StartDt = t.StartDt.Value,
                                                       EndDt = t.EndDt.Value
                                                   });
                // ToDo : TripRecord
                response.Entries = await tripEvent.SingleOrDefaultAsync();

            }
            catch (Exception ex)
            {
                response.StatusCode = EnumStatusCode.Fail;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<TripRecordDataResponse>> GetTripRecord(Base args)
        {
            var response = new ResponseBase<TripRecordDataResponse>()
            {
                Entries = new TripRecordDataResponse(),
            };

            try
            {

                var tripRecord = _userRecordRepository.Get(r => r.RecordId == args.Id)
                                .Join(_locationRepository.Get(), record => record.LocationId, loc => loc.LocationId,
                                      (record,loc)=> new TripRecordDataResponse 
                                      {
                                          RecordId= record.RecordId,
                                          TripId = record.TripId,
                                          occureDt = record.OccurDt,
                                          LocationTitle = loc.LocationTitle,
                                          Longitude = loc.Longitude,
                                          Latitude = loc.Latitude
                                      });

                response.Entries = await tripRecord.SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                response.StatusCode = EnumStatusCode.Fail;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> SaveTripEvent(SaveTripEventArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {
                #region CheckValid
                #endregion
                //todo
                //新增
                if (args.TripId == 0)
                {

                }
                //修改
                else 
                {

                }
                TripEvent tripEvent = new TripEvent()
                {
                    TripTitle = args.TripTitle,
                    StartDt = DateTime.Parse(args.StartDate),
                    EndDt = DateTime.Parse(args.EndDate)
                };
                _tripEventRepositoy.Insert(tripEvent);
                _tripEventRepositoy.SaveChanges();

                List<TripGroup> members = new List<TripGroup>();
                foreach (var fid in args.FriendsId) 
                {
                    members.Add(new TripGroup() 
                    {
                        TripId = tripEvent.TripId,
                        MemberId = fid
                    });
                }
                _tripGroupRepository.InsertRange(members);
                await _tripGroupRepository.SaveChangesAsync();
                response.Entries.Id = tripEvent.TripId;

            }
            catch (Exception ex)
            {
                response.StatusCode = EnumStatusCode.Fail;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseBase<Base>> SaveUserRecord(SaveUserRecordArgs args)
        {
            var response = new ResponseBase<Base>()
            {
                Entries = new Base(),
            };

            try
            {

                //todo
                //新增
                if (args.UserRecordId == 0)
                {

                }
                //修改
                else
                {

                }
                Location location = new Location()
                {
                    LocationTitle = args.Location,
                    Longitude = args.Longitude,
                    Latitude = args.Latitude
                };
                _locationRepository.Insert(location);
                _locationRepository.SaveChanges();
                UserRecord userRecord = new UserRecord()
                {
                    UserId = args.Payload.UserId,
                    TripId = args.TripId,
                    LocationId = location.LocationId,
                    OccurDt = DateTime.Parse(args.OccurDt),
                };
                _userRecordRepository.Insert(userRecord);
                await _userRecordRepository.SaveChangesAsync();
                response.Entries.Id = userRecord.RecordId;
            }
            catch (Exception ex)
            {
                response.StatusCode = EnumStatusCode.Fail;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
