using Microsoft.Extensions.DependencyInjection;
using NauHelper.Core.Interfaces.Services;
using NauHelper.Core.Services;

namespace NauHelper.Core
{
    public static class CoreServiceCollections
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IStudentService, StudentService>();
            services.AddSingleton<IRoleService, RoleService>();
            return services;
        }
    }
}
