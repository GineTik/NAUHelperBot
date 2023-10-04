using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Dialog.Attributes;
using Telegramper.Dialog.Service;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;

namespace UserInterfaces.Administrator.Executors.StudentGroupCoordination
{
    public class GroupExecutor : BaseStudentGroupCoordinationExecutor
    {
        public GroupExecutor(IStudentGroupCoordinationService groupCoordinationService, IUserSession userSession, IDialogService dialogService) : base(groupCoordinationService, userSession, dialogService)
        {
        }

        [TargetCommand(UserStates = "Role:Administrator")]
        public async Task AddGroup()
        {
            await _dialogService.StartAsync<GroupExecutor>();
        }

        [TargetDialogStep("Введіть назву будь-ласка")]
        public async Task TakeGroupName(string name)
        {
            await SelectItem(
                await Specialties,
                s => s.Id.ToString(),
                s => $"{name} {s.Id}",
                nameof(AddGroupOfSpecialty)
            );
            await _dialogService.EndAsync();
        }

        [TargetCallbackData]
        public async Task AddGroupOfSpecialty(string groupName, int specialtyId)
        {
            await _groupCoordinationService.AddGroupAsync(
                UpdateContext.TelegramUserId!.Value,
                groupName,
                specialtyId);
            await Client.SendTextMessageAsync("Додано");
            await Client.DeleteMessageAsync();
        }

        [TargetCommand("remove_group", UserStates = "Role:Administrator")]
        public async Task RemoveGroupList()
        {
            await SelectItem(
                await Groups,
                g => g.Name,
                g => $"{g.Id} {g.Name}",
                nameof(RemoveGroup)
            );
        }

        [TargetCallbackData]
        public async Task RemoveGroup(int groupId, string groupName)
        {
            await Remove(_groupCoordinationService.RemoveGroupAsync, groupId, groupName);
        }
    }
}
