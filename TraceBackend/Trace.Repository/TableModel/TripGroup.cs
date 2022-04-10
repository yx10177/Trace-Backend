using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("TripGroup")]
    public partial class TripGroup
    {
        [Key]
        public int TripId { get; set; }
        [Key]
        public int MemberId { get; set; }
    }
}
