using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;

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
            modelBuilder.Entity<Specialty>()
                .HasOne<Faculty>()
                .WithMany()
                .HasForeignKey(s => s.FacultyId);

            modelBuilder.Entity<Group>()
                .HasOne<Specialty>()
                .WithMany()
                .HasForeignKey(g => g.SpecialtyId);

            modelBuilder.AddUsersAndRoles();
            modelBuilder.AddSettingKeys();

            base.OnModelCreating(modelBuilder);
        }
    }
}
