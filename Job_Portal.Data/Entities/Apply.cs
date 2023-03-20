using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal.Data.Entities
{
    [Table("Apply")]
    public partial class Apply
    {
        [Key]
        public int Id { get; set; }
        [Column("PostJobID")]
        public int? PostJobId { get; set; }
        [Column("UserID")]
        [StringLength(450)]
        public string? UserId { get; set; }

        [ForeignKey("PostJobId")]
        [InverseProperty("Applies")]
        public virtual PostJob? PostJob { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Applies")]
        public virtual AspNetUser? User { get; set; }
    }
}
