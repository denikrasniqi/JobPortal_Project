using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal.Data.Entities
{
    [Table("PostJob")]
    public partial class PostJob
    {
        public PostJob()
        {
            Applies = new HashSet<Apply>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int JobTypeId { get; set; }
        [StringLength(50)]
        public string? WorkTime { get; set; }
        [Column("Remote_office")]
        [StringLength(50)]
        public string? RemoteOffice { get; set; }
        [StringLength(30)]
        public string? Salary { get; set; }

        [ForeignKey("JobTypeId")]
        [InverseProperty("PostJobs")]
        public virtual JobType JobType { get; set; } = null!;
        [InverseProperty("PostJob")]
        public virtual ICollection<Apply> Applies { get; set; }
    }
}
