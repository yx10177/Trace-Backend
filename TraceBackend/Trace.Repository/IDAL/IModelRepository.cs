using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Repository
{
    public interface IUserRepository:IRepository<User,int>{}
    public interface IUserFriendRepository : IRepository<UserFriend, int> { }
    public interface IUserRecordRepository : IRepository<UserRecord, int> { }
    public interface ITripEventRepository : IRepository<TripEvent, int> { }
    public interface ITripGroupRepository : IRepository<TripGroup, int> { }
    public interface ITripArticleRepository : IRepository<TripArticle, int> { }
    public interface ITripPhotoRepository : IRepository<TripPhoto, int> { }
    public interface ILocationRepository : IRepository<Location, int> { }
}
