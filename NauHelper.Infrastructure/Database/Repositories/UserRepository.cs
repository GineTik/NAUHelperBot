using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Constants;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Infrastructure.Database.EF;
using System.ComponentModel;

namespace NauHelper.Infrastructure.Database.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ISettingRepository _settingRepository;

        public UserRepository(DataContext dataContext, ISettingRepository settingRepository) : base(dataContext)
        {
            _settingRepository = settingRepository;
        }

        public async Task<IEnumerable<User>> GetUsersByGroupIdAsync(int groupId)
        {
            var settingKey = await _settingRepository.GetSettingKeyAsync(SettingKeys.GroupId);

            var settings = _dataContext.Settings.Where(ur => 
                ur.RoleSettingKeyId == settingKey.Id
                && ur.Value == groupId.ToString());

            return await _dataContext.Users
                .Where(u => settings.Any(s => s.UserId == u.TelegramId))
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
        {
            var userRoles = _dataContext.UserRoles.Where(ur => ur.RoleId == roleId);
            return await _dataContext.Users.Where(u => userRoles.Any(ur => ur.UserId == u.TelegramId)).ToListAsync();
        }
    }
}
