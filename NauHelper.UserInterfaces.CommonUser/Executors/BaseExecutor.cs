using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Services.Localization;
using NauHelper.Infrastructure.Database.EF;
using NauHelper.Infrastructure.Database.Repositories;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
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

            //await Client.SendTextMessageAsync(
            //    "Привіт✋\n\n" +
            //    "😋 Це бот, який допоможе тобі організовувати своє навчання легче та швидше.\n\n" +
            //    "Щоб взнати, що цей бот може ↪ /help");
        }

        [TargetCommand]
        public async Task Help()
        {
            await Client.SendTextMessageAsync("Це команда help");
        }

        [TargetCommand]
        public async Task ChangeLanguage(string language)
        {
            var successfully = await _localizationService.ChangeLanguageAsync(
                UpdateContext.TelegramUserId!.Value, 
                language
            );

            var responce = "";

            if (successfully == false)
            {
                responce = await _localizer.GetAsync("ChangeLanguageIsFailed", "ua, en-US");
            }
            else
            {
                responce = await _localizer.GetAsync("ChangeLanguageIsSuccess");
            }

            await Client.SendTextMessageAsync(responce);
        }
    } 
}
