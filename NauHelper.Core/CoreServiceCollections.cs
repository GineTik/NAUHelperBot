using Microsoft.Extensions.DependencyInjection;
using NauHelper.Core.Interfaces.Services;
using NauHelper.Core.Services;

namespace NauHelper.Core
{
    public static class CoreServiceCollections
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStudentGroupCoordinationService, StudentGroupCoordinationService>();
            return services;
        }
    }
}
