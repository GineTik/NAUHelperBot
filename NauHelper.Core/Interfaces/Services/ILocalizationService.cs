namespace NauHelper.Core.Interfaces.Services
{
    public interface ILocalizationService
    {
        public Task<bool> ChangeLanguageAsync(long userId, string language);
        public Task<string> GetActualLanguageAsync(long userId);
    }
}
