using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface ISettingRepository
    {
        Task<IEnumerable<Setting>> GetValuesByKeyAsync(string key);
        Task<RoleSettingKey?> GetSettingKeyAsync(string key);
        Task<string> GetValueByKeyAsync(long userId, string key);
        Task SetValueByKeyAsync(long userId, string key, string value);
    }
}
