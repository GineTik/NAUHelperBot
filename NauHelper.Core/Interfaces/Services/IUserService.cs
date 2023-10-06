using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Role>> GetUserRolesAsync(long userId);
        Task<IEnumerable<Role>> GetExistsRolesAsync();
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
        Task<string> GetRoleNameByIdAsync(int roleId);
        Task<bool> HaveRoleAsync(long userId, int roleId);
        
        Task AttachRoleAsync(long plenipotentiaryUserId, long userId, int roleId);
        Task AttachStudentRoleAsync(long userId);
        Task AttachGroupLeaderRoleAsync(long userId);
        
        Task RemoveAttachedRoleAsync(long plenipotentiaryUserId, long userId, int roleId);
        Task RemoveStudentRoleAsync(long userId);
    }
}
