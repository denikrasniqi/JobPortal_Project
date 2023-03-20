using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal.Data.Entities
{
    [Table("CV")]
    public partial class Cv
    {
        public Cv()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        [StringLength(50)]
        public string Extension { get; set; } = null!;
        public string Path { get; set; } = null!;

        [InverseProperty("Cv")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
