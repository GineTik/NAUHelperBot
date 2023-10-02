using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Services.Localization;
using NauHelper.Infrastructure.Localization;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.CommonUser.Executors
{
    public class SettingExecutor : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly ILocalizationService _localizationService;
        private readonly IAvailibleLanguages _availibleLanguages;

        public SettingExecutor(
            ILocalizer localizer,
            ILocalizationService localizationService,
            IAvailibleLanguages availibleLanguages)
        {
            _localizer = localizer;
            _localizationService = localizationService;
            _availibleLanguages = availibleLanguages;
        }

        [TargetCommand]
        public async Task Settings()
        {
            await Client.SendTextMessageAsync(
                await _localizer.GetAsync("Setting"),
                replyMarkup: new InlineKeyboardMarkup(new[]
                {
                    new[] 
                    { 
                        InlineKeyboardButton.WithCallbackData(
                            await _localizer.GetAsync("ChangeLanguage"), 
                            nameof(SelectLanguageButton)
                        ) 
                    }
                })
            );
        }

        [TargetCallbackData]
        public async Task SelectLanguageButton()
        {
            await Client.AnswerCallbackQueryAsync();

            var markup = new InlineKeyboardBuilder().CallbackButtonList(
                _availibleLanguages.LanguageInfos,
                (info, _) => info.Name,
                (info, _) => $"{nameof(ChangeLanguage)} {info.Code}"
            ).Build();

            await Client.SendTextMessageAsync(
                 await _localizer.GetAsync("SelectLanguage"),
                 replyMarkup: markup
            );
        }

        [TargetCallbackData]
        public async Task ChangeLanguage(string language)
        {
            await _localizationService.ChangeLanguageAsync(UpdateContext.TelegramUserId!.Value, language);
            await Client.AnswerCallbackQueryAsync(await _localizer.GetAsync("ChangeLanguageIsSuccess"));
            await Client.DeleteMessageAsync();
        }
    }
}
