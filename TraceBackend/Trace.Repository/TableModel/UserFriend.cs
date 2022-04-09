using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("UserFriend")]
    public partial class UserFriend
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int FriendId { get; set; }
        public bool IsAccept { get; set; }
        public bool IsDelete { get; set; }
    }
}
