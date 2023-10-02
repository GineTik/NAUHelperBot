using NauHelper.Core.Models;

namespace NauHelper.Core.Services.Roles
{
    public class RoleService : IRoleService
    {
        public Task<IEnumerable<Role>> GetUserRolesAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleAsync(long userId, string name)
        {
            throw new NotImplementedException();
        }
    }
}
