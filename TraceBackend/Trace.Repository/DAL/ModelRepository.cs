using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Repository
{
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }

    public class UserFriendRepository : BaseRepository<UserFriend, int>, IUserFriendRepository
    {
        public UserFriendRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }

    public class UserRecordRepository : BaseRepository<UserRecord, int>, IUserRecordRepository
    {
        public UserRecordRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
    public class TripEventRepository : BaseRepository<TripEvent, int>, ITripEventRepository
    {
        public TripEventRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
    public class TripGroupRepository : BaseRepository<TripGroup, int>, ITripGroupRepository
    {
        public TripGroupRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
    public class TripArticleRepository : BaseRepository<TripArticle, int>, ITripArticleRepository
    {
        public TripArticleRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
    public class TripPhotoRepository : BaseRepository<TripPhoto, int>, ITripPhotoRepository
    {
        public TripPhotoRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
    public class LocationRepository : BaseRepository<Location, int>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
    
}
