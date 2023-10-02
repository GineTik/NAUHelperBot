using NauHelper.Core.Models;

namespace NauHelper.Core.Services.Roles
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetUserRolesAsync(long userId);
        Task SetRoleAsync(long userId, string name);
    }  
}
