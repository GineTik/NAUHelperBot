using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using UserInterfaces.CommonUser.Executors.Dialog;

namespace UserInterfaces.CommonUser.Executors
{
    public class BaseExecutor : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly ILocalizationService _localizationService;

        public BaseExecutor(
            ILocalizer localizer,
            ILocalizationService localizationService)
        {
            _localizer = localizer;
            _localizationService = localizationService;
        }

        [TargetCommand]
        public async Task Start()
        {
            await Client.SendTextMessageAsync(
                await _localizer.GetAsync("Start"),
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButton(
                        await _localizer.GetAsync("GoStartRegistration"), 
                        $"{nameof(RegistrationStudentDialog.StartRegistration)}")
                    .Build()
             );
        }

        [TargetCommand]
        public async Task Help()
        {
            await Client.SendTextMessageAsync(await _localizer.GetAsync("Help"));
        }
    } 
}
