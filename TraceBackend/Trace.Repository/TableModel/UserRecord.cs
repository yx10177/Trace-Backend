using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("UserRecord")]
    public partial class UserRecord
    {
        [Key]
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OccurDt { get; set; }
        public int TripId { get; set; }
    }
}
