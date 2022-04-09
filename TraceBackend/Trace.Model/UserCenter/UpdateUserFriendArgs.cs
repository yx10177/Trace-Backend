using System;
using System.Collections.Generic;
using System.Text;

namespace Trace.Model
{
    public class UpdateUserFriendArgs : RequestBase
    {
        public string FriendAccount { get; set; }
        public bool IsAccept { get; set; } = false;
    }
}
