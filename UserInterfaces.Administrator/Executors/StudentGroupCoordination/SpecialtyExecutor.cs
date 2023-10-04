using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Services;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Dialog.Attributes;
using Telegramper.Dialog.Service;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;

namespace UserInterfaces.Administrator.Executors.StudentGroupCoordination
{
    public class SpecialtyExecutor : BaseStudentGroupCoordinationExecutor
    {
        public SpecialtyExecutor(IStudentGroupCoordinationService groupCoordinationService, IUserSession userSession, IDialogService dialogService) : base(groupCoordinationService, userSession, dialogService)
        {
        }

        [TargetCommand(UserStates = "Role:Administrator")]
        public async Task AddSpecialty()
        {
            await _dialogService.StartAsync<SpecialtyExecutor>();
        }

        [TargetDialogStep("Введіть код будь-ласка")]
        public async Task TakeSpecialtyCode(int code)
        {
            await _userSession.SetAsync(new Specialty
            {
                Id = code
            });
            await _dialogService.NextAsync();
        }

        [TargetDialogStep("Введіть назву будь-ласка")]
        public async Task TakeSpecialtyName(string name)
        {
            await _userSession.SetAsync<Specialty>(s =>
            {
                s.Name = name;
            });
            await SelectItem(
                await Faculties,
                f => f.Name,
                f => $"{f.Id}",
                nameof(AddSpecialtyOfFaculty)
            );
            await _dialogService.NextAsync();
        }

        [TargetCallbackData]
        public async Task AddSpecialtyOfFaculty(int facultyId)
        {
            await Client.DeleteMessageAsync();
            
            var specialty = await _userSession.GetAndRemoveAsync<Specialty>();

            await _groupCoordinationService.AddSpecialtyAsync(
               UpdateContext.TelegramUserId!.Value,
               specialty!.Id,
               specialty!.Name,
               facultyId);

            await Client.SendTextMessageAsync("Додано");
        }

        [TargetCommand("remove_specialty", UserStates = "Role:Administrator")]
        public async Task RemoveSpecialtyList()
        {
            await SelectItem(
                await Specialties,
                s => s.Id.ToString(),
                s => $"{s.Id}",
                nameof(RemoveSpecialty)
            );
        }

        [TargetCallbackData]
        public async Task RemoveSpecialty(int specialtyId)
        {
            await Remove(_groupCoordinationService.RemoveSpecialtyAsync, specialtyId, $"{specialtyId}");
        }
    }
}
