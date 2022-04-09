using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class DeleteFriendsArgs:RequestBase
    {
        public List<int> FriendIds { get; set; }
    }
}
