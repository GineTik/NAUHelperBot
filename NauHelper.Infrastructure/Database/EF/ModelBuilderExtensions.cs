using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Core.Constants;

namespace NauHelper.Infrastructure.Database.EF
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder AddUsersAndRoles(this ModelBuilder builder)
        {
            var roles = new List<Role>();
            foreach (var role in Enum.GetValues<ExistingRoles>())
            {
                roles.Add(new Role
                {
                    Id = (int)role,
                    Name = role.ToString()
                });
            }

            builder.Entity<Role>()
                .HasData(roles);

            builder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRole>()
                .HasData(new[]
                {
                    new UserRole { UserId = 502351239, RoleId = 3 }
                });

            builder.Entity<UserRole>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
                .HasOne<Role>()
                .WithMany()
                .HasForeignKey(ur => ur.RoleId);

            return builder;
        }

        public static ModelBuilder AddSettingKeys(this ModelBuilder builder)
        {
            builder.Entity<RoleSettingKey>()
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

            builder.Entity<Setting>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(s => s.UserId);

            builder.Entity<Setting>()
                .HasOne<RoleSettingKey>()
                .WithMany()
                .HasForeignKey(s => s.RoleSettingKeyId);

            return builder;
        }
    }
}
