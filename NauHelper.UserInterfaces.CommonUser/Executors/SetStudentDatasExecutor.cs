using NauHelper.Core.Constants;
using NauHelper.Core.Interfaces.Localization;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;
using NauHelper.Core.Models;
using Telegram.Bot;
using Telegramper.Core.AdvancedBotClient.Extensions;
using Telegramper.Core.Helpers.Builders;
using Telegramper.Executors.Common.Models;
using Telegramper.Executors.QueryHandlers.Attributes.Targets;
using Telegramper.Sessions.Interfaces;

namespace UserInterfaces.CommonUser.Executors
{
    public class SetStudentDatasExecutor : Executor
    {
        private readonly ILocalizer _localizer;
        private readonly IFacultyRepository _facultyRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IUserSession _userSession;
        private readonly IStudentService _studentService;
        private readonly IRoleService _roleService;

        public SetStudentDatasExecutor(
            ILocalizer localizer,
            IGroupRepository groupRepository,
            IFacultyRepository facultyRepository,
            ISpecialtyRepository specialtyRepository,
            IUserSession userSession,
            IStudentService studentService,
            IRoleService roleService)
        {
            _localizer = localizer;
            _groupRepository = groupRepository;
            _facultyRepository = facultyRepository;
            _specialtyRepository = specialtyRepository;
            _userSession = userSession;
            _studentService = studentService;
            _roleService = roleService;
        }

        [TargetCallbackData]
        public async Task StartRegistration()
        {
            await _roleService.AttachStudentRoleAsync(UpdateContext.TelegramUserId!.Value);

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
            await _userSession.SetAsync(new StudentRegistrationInfo(), info => 
            {
                info.UserId = UpdateContext.TelegramUserId!.Value;
                info.FacultyId = facultyId;
            });
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
            await _userSession.SetAsync<StudentRegistrationInfo>(info => info.SpecialtyId = specialtyId);
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
                    .Build()
            );
        }

        [TargetCallbackData]
        public async Task ApplyGroupAndEndRegistration(int groupId)
        {
            await _userSession.SetAsync<StudentRegistrationInfo>(info => info.GroupId = groupId);
            var studentInfo = await _userSession.GetAndRemoveAsync<StudentRegistrationInfo>();

            await _studentService.SetStudentInfoAsync(UpdateContext.TelegramUserId!.Value, studentInfo!);

            await Client.DeleteMessageAsync();
            await Client.SendTextMessageAsync(await _localizer.GetAsync("RegistrationIsEnded"));
        }
    }
}
