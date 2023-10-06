using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Dialog.Attributes;
using Telegramper.Dialog.Service;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Executors.QueryHandlers.Attributes.Validations;

namespace UserInterfaces.Owner.Executors.Role
{
    public class GiveRoleDialog : Executor
    {
        private readonly IDialogService _dialogService;
        private readonly IUserService _userService;
        private readonly ILocalizer _localizer;

        public GiveRoleDialog(IDialogService dialogService, ILocalizer localizer, IUserService userService)
        {
            _dialogService = dialogService;
            _localizer = localizer;
            _userService = userService;
        }

        [TargetDialogStep("Id користувача (/сancel для скасування)")]
        [RequireMessageNumber(ErrorMessage = "Ви маєте надати Id числом")]
        public async Task TakeUsername(long userId)
        {
            var userRoles = await _userService.GetUserRolesAsync(userId);
            var existsRoles = await _userService.GetExistsRolesAsync();

            var availibleRoleToUser = existsRoles.Except(userRoles);

            await Client.SendTextMessageAsync(
                "Надайте користовачу роль\n" +
                $"Його актуальні ролі: {string.Join(", ", userRoles.Select(r => r.Name))}",
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButtonList(
                        availibleRoleToUser,
                        (role, _) => role.Name,
                        (role, _) => $"{nameof(TakeRole)} {userId} {role.Id}")
                    .Build()
            );

            await _dialogService.NextAsync();
        }

        [TargetCallbackData(UserStates = "Role:Owner")]
        public async Task TakeRole(long userId, int roleId)
        {
            var roleName = await _userService.GetRoleNameByIdAsync(roleId);

            var markup = new InlineKeyboardBuilder()
                .CallbackButton(
                    await _localizer.GetForUserAsync("Accept", userId),
                    $"{nameof(AttachRole)} {roleId} {UpdateContext.TelegramUserId}")
                .Build();

            var requestMessage = await Client.SendTextMessageAsync(
                userId,
                await _localizer.GetForUserAsync("WantTakeRole", userId, roleName.ToString()),
                replyMarkup: markup
            );

            await Client.SendTextMessageAsync(
                $"✅Користовачу надіслано запит на отримання ролі {roleName}",
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButton("Видалити запрос", $"{nameof(CancelRequestToAttachRole)} {requestMessage.MessageId}")
                    .Build()
            );
            await Client.DeleteMessageAsync();
        }

        [TargetCallbackData]
        public async Task AttachRole(int roleId, int whoGiveRoleId)
        {
            await _userService.AttachRoleAsync(
                whoGiveRoleId,
                UpdateContext.TelegramUserId!.Value,
                roleId
            );
            var roleName = await _userService.GetRoleNameByIdAsync(roleId);

            await Client.DeleteMessageAsync();
            await Client.SendTextMessageAsync(await _localizer.GetAsync("RoleAccepted", roleName));

            await Client.SendTextMessageAsync(
                whoGiveRoleId,
                $"Користувач {UpdateContext.User}(id:{UpdateContext.TelegramUserId}) приняв роль {roleName}"
            );
        }

        [TargetCallbackData]
        public async Task CancelRequestToAttachRole(int messageId)
        {
            await Client.DeleteMessageAsync(messageId);
            await Client.DeleteMessageAsync();
            await Client.SendTextMessageAsync("Скасовано надісланий запит");
        }
    }
}
