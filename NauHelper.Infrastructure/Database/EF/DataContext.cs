using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Models;

namespace NauHelper.Infrastructure.Database.EF
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleSettingKey> RoleSettingKeys { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasData(new[]
                {
                    new Role { Id = 1, Name = "Student" },
                    new Role { Id = 2, Name = "Administrator" },
                    new Role { Id = 3, Name = "Owner" },
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
                        Key = "Language", 
                        Type = typeof(string).ToString(), 
                        DefaultValue = "ua"
                    },
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
