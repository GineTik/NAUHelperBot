﻿using NauHelper.Core.Entities;
using NauHelper.Core.Enums;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;

namespace NauHelper.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
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
    }
}