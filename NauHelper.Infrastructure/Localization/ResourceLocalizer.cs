using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Services.Localization;
using System.Resources;
using Telegramper.Core.Context;

namespace NauHelper.Infrastructure.Localization
{
    public class ResourceLocalizer : ILocalizer
    {
        private readonly UpdateContext _updateContext;
        private readonly ISettingRepository _settingRepository;

        public ResourceLocalizer(
            UpdateContextAccessor updateContextAccessor,
            ISettingRepository settingRepository)
        {
            _updateContext = updateContextAccessor.UpdateContext;
            _settingRepository = settingRepository;
        }

        public async Task<string> GetAsync(string key, params string[] args)
        {
            var language = await _settingRepository.GetValueByKeyAsync(
                _updateContext.TelegramUserId!.Value,
                LocalizationService.LANGUAGE_KEY
            );

            return await GetByLanguageAsync(key, language!, args);
        }

        public async Task<string> GetByLanguageAsync(string key, string language, params string[] args)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(language);

            var resourceManager = new ResourceManager(
                $"NauHelper.Infrastructure.Localization.Resources.{language}",
                typeof(ResourceLocalizer).Assembly
            );
            var translatedText = resourceManager.GetString(key)
                ?? throw new InvalidOperationException($"Key {key} is not exist");

            for (int i = 0; i < args.Length; i++)
            {
                translatedText = translatedText.Replace("{" + i + "}", args[i]);
            }

            return await Task.FromResult(translatedText.Replace("\\n", "\n"));
        }
    }
}
