using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Services.Localization;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

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
            await Client.SendTextMessageAsync(await _localizer.GetAsync("Start"));
        }

        [TargetCommand]
        public async Task Help()
        {
            await Client.SendTextMessageAsync("" +
                "/settings - налаштування взаємодії з ботом\n");
        }
    } 
}
