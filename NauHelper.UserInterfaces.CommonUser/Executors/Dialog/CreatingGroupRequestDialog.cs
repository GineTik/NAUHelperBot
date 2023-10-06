using NauHelper.Core.Constants;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Services;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Dialog.Attributes;
using Telegramper.Dialog.Service;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;
using Telegram.Bot;
using UserInterfaces.Administrator.Executors.StudentGroupCoordination;

namespace UserInterfaces.CommonUser.Executors.Dialog
{
    internal class SpecialId
    {
        public int Value { get; set; }
    }

    public class CreatingGroupRequestDialog : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly IUserSession _userSession;

        public CreatingGroupRequestDialog(
            ILocalizer localizer,
            IUserService userService,
            IDialogService dialogService,
            IUserSession userSession)
        {
            _localizer = localizer;
            _userService = userService;
            _dialogService = dialogService;
            _userSession = userSession;
        }

        [TargetCallbackData]
        public async Task YouSureToCreateGroup(int spesialtyId)
        {
            await Client.EditMessageTextAsync(
                await _localizer.GetAsync("SendRequestToCreateGroup"),
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButton(
                        await _localizer.GetAsync("SendRequestToCreateGroupButton"),
                        $"{nameof(StartCreateGroupDialog)} {spesialtyId}")
                    .Build());
        }

        [TargetCallbackData]
        public async Task StartCreateGroupDialog(int specisaltyId)
        {
            await _userSession.SetAsync<SpecialId>(new SpecialId { Value = specisaltyId });
            await _dialogService.StartAsync<CreatingGroupRequestDialog>();
            await Client.DeleteMessageAsync();
        }

        [TargetDialogStep("Введіть нову назву групи")]
        public async Task SendRequestToCreateGroup(string name)
        {
            var spesialtyId = await _userSession.GetAndRemoveAsync<SpecialId>();
            var users = await _userService.GetUsersByRoleIdAsync((int)ExistingRoles.Owner);
            foreach (var user in users)
            {
                await Client.SendTextMessageAsync(
                    user.TelegramId,
                    $"💥 Поступив запрос на створення групи під назвою {name}",
                    replyMarkup: new InlineKeyboardBuilder()
                        .CallbackButton(
                            "Створити",
                            $"{nameof(GroupExecutor.AddGroupOfSpecialty)} {name} {spesialtyId!.Value}")
                        .Build());
            }
            await Client.SendTextMessageAsync(await _localizer.GetAsync("RequestToCreateGroupSended", name));
            await _dialogService.NextAsync();
        }
    }
}
