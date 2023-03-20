using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal.Data.Entities
{
    [Index("NormalizedEmail", Name = "EmailIndex")]
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            Applies = new HashSet<Apply>();
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Roles = new HashSet<AspNetRole>();
        }

        [Key]
        public string Id { get; set; } = null!;
        [StringLength(150)]
        public string? Name { get; set; }
        [StringLength(150)]
        public string? Surname { get; set; }
        [StringLength(256)]
        public string? UserName { get; set; }
        [StringLength(256)]
        public string? NormalizedUserName { get; set; }
        [StringLength(256)]
        public string? Email { get; set; }
        [StringLength(256)]
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int? PictureId { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        [StringLength(20)]
        public string? Gender { get; set; }
        public string? Experience { get; set; }
        public string? Education { get; set; }
        public string? Skills { get; set; }
        public string? Projects { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
        public string? GitHub { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Birthday { get; set; }
        public int? CvId { get; set; }

        [ForeignKey("CvId")]
        [InverseProperty("AspNetUsers")]
        public virtual Cv? Cv { get; set; }
        [ForeignKey("PictureId")]
        [InverseProperty("AspNetUsers")]
        public virtual UserPicture? Picture { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Apply> Applies { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Users")]
        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
