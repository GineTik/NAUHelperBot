using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Models;
using NauHelper.Infrastructure.Database.EF;
using Telegramper.Core.Context;

namespace NauHelper.Infrastructure.Database.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly DataContext _dataContext;

        public SettingRepository(
            DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> GetValueByKeyAsync(long userId, string key)
        {
            var settingKey = await _dataContext.RoleSettingKeys.FirstOrDefaultAsync(
                settingKey => settingKey.Key == "Language"
            );

            var settingValue = await _dataContext.Settings.FirstOrDefaultAsync(setting =>
                setting.UserId == userId
                && setting.RoleSettingKeyId == settingKey!.Id
            );

            var language = settingValue?.Value ?? settingKey!.DefaultValue
                ?? throw new InvalidOperationException("Language is null in the configuration file");

            return language;
        }

        public async Task SetValueByKeyAsync(long userId, string key, string value)
        {
            var settingKey = await _dataContext.RoleSettingKeys.FirstOrDefaultAsync(settingKey =>
                settingKey.Key == key
            );

            var settingValue = await _dataContext.Settings.FirstOrDefaultAsync(setting =>
                setting.UserId == userId
                && setting.RoleSettingKeyId == settingKey!.Id
            );

            if (settingValue == null)
            {
                _dataContext.Settings.Add(new Setting
                {
                    UserId = userId,
                    RoleSettingKeyId = settingKey!.Id,
                    Value = value
                });
            }
            else
            {
                settingValue.Value = value;
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}
