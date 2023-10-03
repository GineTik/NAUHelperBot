using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetUserRolesAsync(long userId);
        Task<bool> HaveRoleAsync(long userId, int roleId);
        Task AttachRoleAsync(long plenipotentiaryUserId, long userId, int roleId);
        Task AttachStudentRoleAsync(long userId);
    }
}
