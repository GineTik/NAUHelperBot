using NauHelper.Core.Constants;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;
using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Dialog.Attributes;
using Telegramper.Dialog.Service;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;
using UserInterfaces.Administrator.Executors.StudentGroupCoordination;

namespace UserInterfaces.CommonUser.Executors
{
    internal class SpecialId
    {
        public int Value { get; set; }
    }

    public class SetStudentDatasExecutor : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        private readonly IDialogService _dialogService;
        private readonly IUserSession _userSession;

        public SetStudentDatasExecutor(
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
                        rowCount: 2
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
                        $"{nameof(CreateRequestToAddGroup)} {specialtyId}")
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

        [TargetCallbackData]
        public async Task CreateRequestToAddGroup(int spesialtyId)
        {
            await Client.SendTextMessageAsync(
                await _localizer.GetAsync("SendRequestToCreateGroup"),
                replyMarkup: new InlineKeyboardBuilder()
                    .CallbackButton(
                        await _localizer.GetAsync("SendRequestToCreateGroupButton"), 
                        $"{nameof(CreateRequestToAddGroupAprove)} {spesialtyId}")
                    .Build());
        }

        [TargetCallbackData]
        public async Task CreateRequestToAddGroupAprove(int specisaltyId)
        {
            await _userSession.SetAsync<SpecialId>(new SpecialId { Value = specisaltyId });
            await _dialogService.StartAsync<SetStudentDatasExecutor>();
        }

        [TargetDialogStep("Введіть нову назву групи")]
        public async Task SendMessageToModeratorForApplyCreatingGroup(string name)
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
            await Client.SendTextMessageAsync($"Запрос на створення групи під назвою {name} відправлено.");
            await _dialogService.NextAsync();
        }
    }
}
