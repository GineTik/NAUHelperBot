using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Dialog.Service;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;

namespace UserInterfaces.CommonUser.Executors.Dialog
{
    public class RegistrationStudentDialog : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly IUserSession _userSession;

        public RegistrationStudentDialog(
            ILocalizer localizer,
            IGroupRepository groupRepository,
            IFacultyRepository facultyRepository,
            ISpecialtyRepository specialtyRepository,
            IStudentService studentService,
            IUserService userService,
            IDialogService dialogService,
            IUserSession userSession)
        {
            _localizer = localizer;
            _groupRepository = groupRepository;
            _facultyRepository = facultyRepository;
            _specialtyRepository = specialtyRepository;
            _studentService = studentService;
            _userService = userService;
            _dialogService = dialogService;
            _userSession = userSession;
        }

        [TargetCallbackData]
        public async Task StartRegistration()
        {
            var allFaculties = await _facultyRepository.GetAllAsync();

            await Client.SendTextMessageAsync(
                await _localizer.GetAsync("SelectFaculty"),
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButtonList(
                        allFaculties,
                        (f, _) => f.Name,
                        (f, _) => $"{nameof(ApplyFacultyAndSelectSpecialty)} {f.Id}",
                        rowCount: 2
                     )
                    .Build()
            );
        }

        [TargetCallbackData]
        public async Task ApplyFacultyAndSelectSpecialty(int facultyId)
        {
            var specialties = await _specialtyRepository.GetByFacultyIdAsync(facultyId);

            await Client.EditMessageTextAsync(
                await _localizer.GetAsync("SelectSpecialty"),
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButtonList(
                        specialties,
                        (s, _) => s.Id.ToString(),
                        (s, _) => $"{nameof(ApplySpecialtyAndSelectGroup)} {s.Id}",
                        rowCount: 3
                     )
                    .Build()
            );
        }

        [TargetCallbackData]
        public async Task ApplySpecialtyAndSelectGroup(int specialtyId)
        {
            var allGroups = await _groupRepository.GetBySpecialtyIdAsync(specialtyId);

            await Client.EditMessageTextAsync(
                await _localizer.GetAsync("SelectGroup"),
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButtonList(
                        allGroups,
                        (g, _) => g.Name,
                        (g, _) => $"{nameof(ApplyGroupAndEndRegistration)} {g.Id}",
                        rowCount: 4
                     )
                    .CallbackButton(
                        await _localizer.GetAsync("MyGroupIsMissing"),
                        $"{nameof(CreatingGroupRequestDialog.YouSureToCreateGroup)} {specialtyId}")
                    .EndRow()
                    .Build()
            );
        }

        [TargetCallbackData]
        public async Task ApplyGroupAndEndRegistration(int groupId)
        {
            await _studentService.ApplyRegistrationAsync(UpdateContext.TelegramUserId!.Value, groupId);

            await Client.DeleteMessageAsync();
            await Client.SendTextMessageAsync(await _localizer.GetAsync("RegistrationIsEnded"));
        }
    }
}
