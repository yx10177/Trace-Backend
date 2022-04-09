using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Trace.Repository
{
    [Table("TripArticle")]
    public partial class TripArticle
    {
        [Key]
        public int RecordId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string ArticleContent { get; set; }
        [Column("UpdateDT", TypeName = "datetime")]
        public DateTime? UpdateDt { get; set; }
    }
}
