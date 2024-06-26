using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Core.Entities;

namespace TimeKeeper.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AttendanceEntry> AttendanceEntries { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(p => p.Id);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => ur.RoleId);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => new { rp.RoleId, rp.PermissionId });

                entity.HasOne(rp => rp.Role)
                      .WithMany()
                      .HasForeignKey(rp => rp.RoleId);

                entity.HasOne(rp => rp.Permission)
                      .WithMany()
                      .HasForeignKey(rp => rp.PermissionId);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(ud => ud.UserId);
                entity.HasOne(ud => ud.Role)
                      .WithMany()
                      .HasForeignKey(ud => ud.RoleId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.UserDetails)
                      .WithMany()
                      .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "CanEdit" },
                new Permission { Id = 2, Name = "CanExportPDF" }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { RoleId = 1, RoleName = "Admin" },
                new UserRole { RoleId = 2, RoleName = "Staff" }
            );

            modelBuilder.Entity<RolePermission>().HasData(
              new RolePermission { RoleId = 1, PermissionId = 1 },
              new RolePermission { RoleId = 1, PermissionId = 2 }
            );
        }
    }
}
