using Microsoft.Extensions.DependencyInjection;
using NauHelper.Core;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Services.Localization;
using NauHelper.Infrastructure;
using NauHelper.Infrastructure.Database.EF;
using NauHelper.Infrastructure.Database.Repositories;
using NauHelper.Infrastructure.Localization;
using Telegram.Bot.Types.Enums;
using Telegramper.Core;
using Telegramper.Core.Configuration.ReceiverOption;
using Telegramper.Executors.Initialization.Services;
using Telegramper.Executors.QueryHandlers.Middleware;
using UserInterfaces.CommonUser.Middlewares;

namespace NauHelper.Startup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = BotApplicationBuilder.CreateBuilder();
            builder.ReceiverOptions.ConfigureAllowedUpdates(UpdateType.Message, UpdateType.CallbackQuery);
            builder.Services.AddExecutors(
                assemblies: UserInterfacesAssemblies.Assemblies, 
                options =>
                {
                    options.ParameterParser.ErrorMessages.ArgsLengthIsLess = "Недостатньо аргументів";
                    options.ParameterParser.ErrorMessages.TypeParseError = "Некоректні дані";
                }
            );
            builder.Services.AddCore();
            builder.Services.AddInfrastracture();

            var app = builder.Build();
            app.UseMiddleware<RegistrationUserMiddleware>();
            app.UseExecutors();
            app.RunPolling();
        }
    }
}