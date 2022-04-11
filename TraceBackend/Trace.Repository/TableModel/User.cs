using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(20)]
        public string UserAccount { get; set; }
        [StringLength(50)]
        public string UserPassword { get; set; }
        [StringLength(50)]
        public string UserEmail { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string UserPhotoPath { get; set; }
    }
}
