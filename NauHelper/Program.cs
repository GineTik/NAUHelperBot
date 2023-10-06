using NauHelper.Core;
using NauHelper.Infrastructure;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegramper.Core;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Configuration.Middlewares.CatchException;
using Telegramper.Core.Configuration.ReceiverOption;
using Telegramper.Dialog;
using Telegramper.Executors.Initialization.Services;
using Telegramper.Executors.QueryHandlers.Middleware;
using Telegramper.Sessions.Extensions.Services;
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

            builder.Services.AddDialogsByAttributes();
            builder.Services.AddSessions();

            var app = builder.Build();
            app.UseCatchException(async (updateContext, ex) =>
            {
                await updateContext.Client.SendTextMessageAsync(ex.Message);
                Console.WriteLine(ex);
            });
            app.UseCatchException<NullReferenceException>(async (updateContext, ex) =>
            {
                await updateContext.Client.SendTextMessageAsync(ex.Message);
                Console.WriteLine(ex);
            });
            app.UseMiddleware<RegistrationUserMiddleware>();
            app.UseMiddleware<AttachRoleMiddleware>();
            app.UseExecutors();
            app.RunPolling();
        }
    }
}