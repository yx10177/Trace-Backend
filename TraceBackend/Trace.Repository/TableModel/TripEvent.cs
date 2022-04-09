using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("TripEvent")]
    public partial class TripEvent
    {
        [Key]
        public int TripId { get; set; }
        [StringLength(50)]
        public string TripTitle { get; set; }
    }
}
