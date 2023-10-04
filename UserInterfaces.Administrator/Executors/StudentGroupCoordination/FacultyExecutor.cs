using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Dialog.Attributes;
using Telegramper.Dialog.Service;
using Telegramper.Executors.QueryHandlers.Attributes.ParametersParse.Separator;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;

namespace UserInterfaces.Administrator.Executors.StudentGroupCoordination
{
    public class FacultyExecutor : BaseStudentGroupCoordinationExecutor
    {
        public FacultyExecutor(IStudentGroupCoordinationService groupCoordinationService, IUserSession userSession, IDialogService dialogService) : base(groupCoordinationService, userSession, dialogService)
        {
        }

        [TargetCommand(UserStates = "Role:Administrator")]
        public async Task AddFaculty()
        {
            await _dialogService.StartAsync<FacultyExecutor>();
        }

        [TargetDialogStep("Введіть назву будь-ласка")]
        [EmptyParametersSeparator]
        public async Task TakeFacultyName(string name)
        {
            await _groupCoordinationService.AddFacultyAsync(
                UpdateContext.TelegramUserId!.Value,
                name);
            await Client.SendTextMessageAsync("Додано");
            await _dialogService.EndAsync();
        }

        [TargetCommand("remove_faculty", UserStates = "Role:Administrator")]
        public async Task RemoveFacultyList()
        {
            await SelectItem(
                await Faculties,
                f => f.Name,
                f => $"{f.Id} {f.Name}",
                nameof(RemoveFaculty)
            );
        }

        [TargetCallbackData]
        public async Task RemoveFaculty(int facultyId, string facultyName)
        {
            await Remove(_groupCoordinationService.RemoveFacultyAsync, facultyId, facultyName);
        }
    }
}
