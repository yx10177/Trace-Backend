using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("Location")]
    public partial class Location
    {
        [Key]
        public int LocationId { get; set; }
        [Required]
        [StringLength(50)]
        public string LocationTitle { get; set; }
        [Column(TypeName = "decimal(10, 7)")]
        public decimal? Longitude { get; set; }
        [Column(TypeName = "decimal(10, 7)")]
        public decimal? Latitude { get; set; }
    }
}
