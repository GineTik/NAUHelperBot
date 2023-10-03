using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetUserRolesAsync(long userId);
        Task<IEnumerable<Role>> GetExistsRolesAsync();
        Task<Role> GetByIdAsync(long roleId);
        Task AttachRoleAsync(long userId, int roleId);
        Task RemoveAttachedRoleAsync(long userId, int roleId);
    }
}
