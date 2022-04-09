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
}
