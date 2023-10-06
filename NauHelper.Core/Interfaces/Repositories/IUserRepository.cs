using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
        Task<IEnumerable<User>> GetUsersByGroupIdAsync(int groupId);
    }
}
