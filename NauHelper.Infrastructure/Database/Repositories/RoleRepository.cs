using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Models;
using NauHelper.Infrastructure.Database.EF;

namespace NauHelper.Infrastructure.Database.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _dataContext;

        public RoleRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AttachRoleAsync(long userId, int roleId)
        {
            var role = await _dataContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId)
                ?? throw new InvalidDataException($"Role(id:{roleId}) don't exists");

            var userRole = await _dataContext.UserRoles.FirstOrDefaultAsync(ur => 
                ur.UserId == userId
                && ur.RoleId == roleId
            );

            if (userRole != null) 
            {
                return;
            }

            _dataContext.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Role> GetByIdAsync(long roleId)
        {
            return await _dataContext.Roles.FirstAsync(r => r.Id == roleId);
        }

        public async Task<IEnumerable<Role>> GetExistsRolesAsync()
        {
            return await _dataContext.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(long userId)
        {
            var userRoles = await _dataContext.UserRoles.Where(ur => ur.UserId == userId).ToListAsync();

            var roles = new List<Role>();
            foreach (var userRole in userRoles) 
            {
                roles.Add(await _dataContext.Roles.FirstAsync(r => r.Id == userRole.RoleId));
            }
            return roles;
        }

        public async Task RemoveAttachedRoleAsync(long userId, int roleId)
        {
            var userRole = await _dataContext.UserRoles.FirstOrDefaultAsync(ur =>
                ur.UserId == userId
                && ur.RoleId == roleId
            );

            if (userRole == null)
            {
                return;
            }

            _dataContext.UserRoles.Remove(userRole);
            await _dataContext.SaveChangesAsync();
        }
    }
}
