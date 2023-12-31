﻿using Microsoft.Extensions.DependencyInjection;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Infrastructure.Database.EF;
using NauHelper.Infrastructure.Database.Repositories;
using NauHelper.Infrastructure.Localization;

namespace NauHelper.Infrastructure
{
    public static class InfrastractureServiceCollections
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(DataContextFactory.ConfigureOptions);
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IFacultyRepository, FacultyRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<ISpecialtyRepository, SpecialtyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton<IAvailibleLanguages, AvailibleLanguages>();
            services.AddScoped<ILocalizer, ResourceLocalizer>();
            return services;
        }
    }
}
