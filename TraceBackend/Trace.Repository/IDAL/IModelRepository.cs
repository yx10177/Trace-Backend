using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Repository
{
    public interface IUserRepository:IRepository<User,int>{}
    public interface IUserFriendRepository : IRepository<UserFriend, int> { }
    public interface IUserRecordRepository : IRepository<UserRecord, int> { }

}
