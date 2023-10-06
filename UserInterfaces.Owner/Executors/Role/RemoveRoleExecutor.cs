using NauHelper.Core.Constants;
using NauHelper.Core.Interfaces.Repositories;
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
    public class RemoveRoleExecutor : Executor
    {
        private readonly IDialogService _dialogService;
        private readonly IUserService _roleService;

        public RemoveRoleExecutor(IDialogService dialogService, IUserService roleService)
        {
            _dialogService = dialogService;
            _roleService = roleService;
        }

        [TargetCommand(UserStates = "Dialog:RemoveRoleExecutor")]
        public async Task Сancel()
        {
            await _dialogService.EndAsync();
            await Client.SendTextMessageAsync("Скасовано");
        }

        [TargetCommand(UserStates = "Role:Owner")]
        public async Task RemoveRole()
        {
            await _dialogService.StartAsync<RemoveRoleExecutor>();
        }

        [TargetDialogStep("Id користувача (/сancel для скасування)")]
        [RequireMessageNumber(ErrorMessage = "Ви маєте надати Id числом")]
        public async Task TakeUsername(long userId)
        {
            var userRoles = await _roleService.GetUserRolesAsync(userId);

            await Client.SendTextMessageAsync(
                "Виберіть роль для видалення",
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButtonList(
                        userRoles,
                        (role, _) => role.Name,
                        (role, _) => $"{nameof(RemoveRoleButton)} {userId} {role.Id}"
                    )
                    .Build()
            );

            await _dialogService.NextAsync();
        }

        [TargetCallbackData(UserStates = "Role:Owner")]
        public async Task RemoveRoleButton(long userId, int roleId)
        {
            var roleName = await _roleService.GetRoleNameByIdAsync(roleId);
            await _roleService.RemoveAttachedRoleAsync(UpdateContext.TelegramUserId!.Value, userId, roleId);
            await Client.SendTextMessageAsync($"✅Роль {roleName} видалена.");
            await Client.DeleteMessageAsync();
        }
    }
}
