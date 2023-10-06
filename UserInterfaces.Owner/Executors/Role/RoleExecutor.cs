using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Dialog.Service;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;

namespace UserInterfaces.Owner.Executors.Role
{
    public class RoleExecutor : Executor
    {
        private readonly IDialogService _dialogService;

        public RoleExecutor(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [TargetCommand(UserStates = "Role:Owner")]
        public async Task AttachRole()
        {
            await _dialogService.StartAsync<GiveRoleDialog>();
        }

        [TargetCommand(UserStates = "Role:Owner")]
        public async Task RemoveRole()
        {
            await _dialogService.StartAsync<RemoveRoleDialog>();
        }

        [TargetCommand(UserStates = $"Dialog:{nameof(GiveRoleDialog)}")]
        [TargetCommand(UserStates = $"Dialog:{nameof(RemoveRoleDialog)}")]
        public async Task Cancel()
        {
            await _dialogService.EndAsync();
            await Client.SendTextMessageAsync("Скасовано");
        }
    }
}
