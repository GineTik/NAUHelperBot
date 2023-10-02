namespace NauHelper.Core.Services.Localization
{
    public interface ILocalizationService
    {
        public Task<bool> ChangeLanguageAsync(long userId, string language);
    }
}
