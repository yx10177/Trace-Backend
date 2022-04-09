using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("TripPhoto")]
    public partial class TripPhoto
    {
        [Key]
        public int PhotoId { get; set; }
        public int RecordId { get; set; }
        [Required]
        [StringLength(50)]
        public string PhotoPath { get; set; }
    }
}
