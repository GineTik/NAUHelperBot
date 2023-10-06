using NauHelper.Core.Constants;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;

namespace NauHelper.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Role>> GetUserRolesAsync(long userId)
        {
            return await _roleRepository.GetUserRolesAsync(userId);
        }

        public async Task AttachRoleAsync(long plenipotentiaryUserId, long userId, int roleId)
        {
            if (await HaveRoleAsync(plenipotentiaryUserId, (int)ExistingRoles.Owner) == false)
            {
                throw new InvalidOperationException("User haven't owner role");
            }

            await _roleRepository.AttachRoleAsync(userId, roleId);
        }

        public async Task<bool> HaveRoleAsync(long userId, int roleId)
        {
            var userRoles = await GetUserRolesAsync(userId);
            return userRoles.Any(r => r.Id == roleId);
        }

        public async Task AttachStudentRoleAsync(long userId)
        {
            await _roleRepository.AttachRoleAsync(userId, (int)ExistingRoles.Student);
        }

        public Task<IEnumerable<Role>> GetExistsRolesAsync()
        {
            return  _roleRepository.GetExistsRolesAsync();
        }

        public async Task<string> GetRoleNameByIdAsync(int roleId)
        {
            var name = ((ExistingRoles)roleId).ToString();
            return await Task.FromResult(name);
        }

        public async Task RemoveAttachedRoleAsync(long plenipotentiaryUserId, long userId, int roleId)
        {
            if (await HaveRoleAsync(plenipotentiaryUserId, (int)ExistingRoles.Owner) == false)
            {
                throw new InvalidOperationException("User haven't owner role");
            }

            await _roleRepository.RemoveAttachedRoleAsync(userId, roleId);
        }

        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _userRepository.GetUsersByRoleIdAsync(roleId);
        }

        public async Task AttachGroupLeaderRoleAsync(long userId)
        {
            await _roleRepository.AttachRoleAsync(userId, (int)ExistingRoles.GroupLeader);
        }
    }
}
