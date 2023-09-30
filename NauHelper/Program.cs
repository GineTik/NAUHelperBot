using Telegram.Bot.Types.Enums;
using Telegramper.Core;
using Telegramper.Core.Configuration.ReceiverOption;
using Telegramper.Executors.Initialization.Services;
using Telegramper.Executors.QueryHandlers.Middleware;

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

            var app = builder.Build();
            app.UseExecutors();
            app.RunPolling();
        }
    }
}