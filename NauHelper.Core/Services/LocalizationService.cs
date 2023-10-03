using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;

namespace NauHelper.Core.Services
{
    public class LocalizationService : ILocalizationService
    {
        public const string LANGUAGE_KEY = "Language";

        private readonly ISettingRepository _settingRepository;
        private readonly IAvailibleLanguages _availibleLanguages;

        public LocalizationService(ISettingRepository settingRepository, IAvailibleLanguages availibleLanguages)
        {
            _settingRepository = settingRepository;
            _availibleLanguages = availibleLanguages;
        }

        public async Task<bool> ChangeLanguageAsync(long userId, string language)
        {
            if (_availibleLanguages.LanguageInfos.Any(info => info.Code == language) == false)
            {
                return false;
            }

            await _settingRepository.SetValueByKeyAsync(
                userId,
                LANGUAGE_KEY,
                language
            );

            return true;
        }

        public async Task<string> GetActualLanguageAsync(long userId)
        {
            return await _settingRepository.GetValueByKeyAsync(
                userId,
                LANGUAGE_KEY
            );
        }
    }
}
