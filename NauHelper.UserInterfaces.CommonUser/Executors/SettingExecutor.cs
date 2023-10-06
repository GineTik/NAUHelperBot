using NauHelper.Core.Constants;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Executors.QueryHandlers.UserState;
using UserInterfaces.CommonUser.Executors.Dialog;

namespace UserInterfaces.CommonUser.Executors
{
    public class SettingExecutor : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly ILocalizationService _localizationService;
        private readonly IAvailibleLanguages _availibleLanguages;
        private readonly IUserStates _userStates;
        private readonly ISettingRepository _settingRepository;
        private readonly IStudentService _studentService;

        public SettingExecutor(
            ILocalizer localizer,
            ILocalizationService localizationService,
            IAvailibleLanguages availibleLanguages,
            IUserStates userStates,
            ISettingRepository settingRepository,
            IStudentService studentService)
        {
            _localizer = localizer;
            _localizationService = localizationService;
            _availibleLanguages = availibleLanguages;
            _userStates = userStates;
            _settingRepository = settingRepository;
            _studentService = studentService;
        }

        [TargetCommand]
        public async Task Settings()
        {
            var text = await _localizer.GetAsync("Setting");

            var userStates = await _userStates.GetAsync();
            if (userStates.Contains("Role:" + nameof(ExistingRoles.Student)))
            {
                var studentInfo = await _studentService.GetStudentInfoAsync(UpdateContext.TelegramUserId!.Value);
                text += "\n" + await _localizer.GetAsync(
                    "StudentSettingsDatas",
                    studentInfo.Faculty.Name,
                    $"{studentInfo.Specialty.Id} \"{studentInfo.Specialty.Name}\"",
                    studentInfo.Group.Name
                );
            }

            await Client.SendTextMessageAsync(
                text,
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButton(
                        await _localizer.GetAsync("ChangeLanguage"), 
                        nameof(ChangeLanguageDialog.SelectLanguageButton))
                    .EndRow()
                    .CallbackButton(
                        await _localizer.GetAsync("Re-registration"),
                        $"{nameof(RegistrationStudentDialog.StartRegistration)}")
                    .EndRow()
                    .Build()
            );
        }
    }
}
