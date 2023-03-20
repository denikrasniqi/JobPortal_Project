using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Job_Portal.Data.Entities;

namespace Job_Portal.Data.Context
{
    public partial class Job_PortalContext : DbContext
    {
        public Job_PortalContext()
        {
        }

        public Job_PortalContext(DbContextOptions<Job_PortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apply> Applies { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Cv> Cvs { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<PostJob> PostJobs { get; set; } = null!;
        public virtual DbSet<UserPicture> UserPictures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DIELLZAHYSENI;Initial Catalog=Job_Portal;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apply>(entity =>
            {
                entity.HasOne(d => d.PostJob)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.PostJobId)
                    .HasConstraintName("FK_Apply_PostJob");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Applies)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Apply_AspNetUsers");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Gender).IsFixedLength();

                entity.HasOne(d => d.Cv)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.CvId)
                    .HasConstraintName("FK_AspNetUsers_CV");

                entity.HasOne(d => d.Picture)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.PictureId)
                    .HasConstraintName("FK_AspNetUsers_UserPicture");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<PostJob>(entity =>
            {
                entity.Property(e => e.RemoteOffice).IsFixedLength();

                entity.Property(e => e.Salary).IsFixedLength();

                entity.Property(e => e.WorkTime).IsFixedLength();

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.PostJobs)
                    .HasForeignKey(d => d.JobTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostJob_JobType");
            });

            modelBuilder.Entity<UserPicture>(entity =>
            {
                entity.Property(e => e.Extension).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
