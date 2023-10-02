using Microsoft.Extensions.DependencyInjection;
using NauHelper.Core.Services.Localization;

namespace NauHelper.Core
{
    public static class CoreServiceCollections
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizationService, LocalizationService>();
            return services;
        }
    }
}
