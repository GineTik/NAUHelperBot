using Microsoft.Extensions.Configuration;
using NauHelper.Core.Localization;
using System.Resources;

namespace NauHelper.Infrastructure.Localization
{
    public class ResourceLocalizer : ILocalizer
    {
        private const string LANGUAGE_KEY = "Language";

        private readonly IConfiguration _configuration;

        public ResourceLocalizer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string key)
        {
            var language = _configuration[LANGUAGE_KEY];
            if (language == null)
            {
                throw new InvalidOperationException("Language is null in the configuration file");
            }
            return Get(key, language!);
        }

        public string Get(string key, string language)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(language);

            var resourceManager = new ResourceManager(
                $"NauHelper.Infrastructure.Localization.Resources.{language}",
                typeof(ResourceLocalizer).Assembly
            );
            return resourceManager.GetString(key)
                ?? throw new InvalidOperationException($"Key {key} is not exist");
        }
    }
}
