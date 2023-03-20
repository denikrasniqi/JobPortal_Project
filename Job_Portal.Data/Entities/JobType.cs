using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal.Data.Entities
{
    [Table("JobType")]
    public partial class JobType
    {
        public JobType()
        {
            PostJobs = new HashSet<PostJob>();
        }

        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = null!;

        [InverseProperty("JobType")]
        public virtual ICollection<PostJob> PostJobs { get; set; }
    }
}
