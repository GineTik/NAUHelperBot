namespace NauHelper.Core.Interfaces.Localization
{
    public interface ILocalizer
    {
        Task<string> GetAsync(string key, params string[] args);
        Task<string> GetByLanguageAsync(string key, string lang, params string[] args);
    }
}
