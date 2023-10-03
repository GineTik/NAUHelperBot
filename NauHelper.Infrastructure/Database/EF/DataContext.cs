using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Core.Enums;

namespace NauHelper.Infrastructure.Database.EF
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleSettingKey> RoleSettingKeys { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Specialty> Specialties { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new[]
                {
                    new Role { Id = (int)ExistingRoles.Student, Name = "Student" },
                    new Role { Id = (int)ExistingRoles.Administrator, Name = "Administrator" },
                    new Role { Id = (int)ExistingRoles.Owner, Name = "Owner" },
                });

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasData(new[]
                {
                    new UserRole { UserId = 502351239, RoleId = 3 }
                });

            modelBuilder.Entity<RoleSettingKey>()
                .HasData(new[]
                {
                    new RoleSettingKey { 
                        Id = 1, 
                        Key = SettingKeys.Language, 
                        Type = typeof(string).ToString(), 
                        DefaultValue = "ua"
                    },
                    new RoleSettingKey {
                        Id = 2,
                        RoleId = (int)ExistingRoles.Student,
                        Key = SettingKeys.FacultyId,
                        Type = typeof(int).ToString(),
                        DefaultValue = ""
                    },
                    new RoleSettingKey {
                        Id = 3,
                        RoleId = (int)ExistingRoles.Student,
                        Key = SettingKeys.GroupId,
                        Type = typeof(int).ToString(),
                        DefaultValue = ""
                    },
                    new RoleSettingKey {
                        Id = 4,
                        RoleId = (int)ExistingRoles.Student,
                        Key = SettingKeys.SpecialtyId,
                        Type = typeof(int).ToString(),
                        DefaultValue = ""
                    },
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
