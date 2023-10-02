namespace NauHelper.Core.Interfaces.Repositories
{
    public interface ISettingRepository
    {
        Task<string> GetValueByKeyAsync(long userId, string key);
        Task SetValueByKeyAsync(long userId, string key, string value);
    }
}
