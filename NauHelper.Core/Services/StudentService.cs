using NauHelper.Core.Constants;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;
using NauHelper.Core.Models;

namespace NauHelper.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly ISettingRepository _settingRepository;
        private readonly IFacultyRepository _facultyRepository;
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserService _userService;
        private readonly IStudentGroupCoordinationService _groupCoordinationService;

        public StudentService(ISettingRepository settingRepository, IFacultyRepository facultyRepository, ISpecialtyRepository specialtyRepository, IGroupRepository groupRepository, IUserService userService, IStudentGroupCoordinationService groupCoordinationService)
        {
            _settingRepository = settingRepository;
            _facultyRepository = facultyRepository;
            _specialtyRepository = specialtyRepository;
            _groupRepository = groupRepository;
            _userService = userService;
            _groupCoordinationService = groupCoordinationService;
        }

        public async Task<StudentInfo> GetStudentInfoAsync(long userId)
        {
            if (await _userService.HaveRoleAsync(userId, (int)ExistingRoles.Student) == false)
            {
                throw new InvalidDataException($"User({userId}) haven't Student role");
            }

            var groupId = await _settingRepository.GetValueByKeyAsync(
                userId,
                SettingKeys.GroupId
            );

            var group = await _groupRepository.GetByIdAsync(int.Parse(groupId));

            if (group == null)
            {
                await _settingRepository.SetValueByKeyAsync(
                    userId,
                    SettingKeys.GroupId,
                    ""
                );
                throw new InvalidDataException($"GroupId({groupId}) in the settings is incorrect");
            }

            var specialty = await _specialtyRepository.GetByIdAsync(group!.SpecialtyId);
            var faculty = await _facultyRepository.GetByIdAsync(specialty!.FacultyId);

            return new StudentInfo
            {
                Faculty = faculty!,
                Specialty = specialty,
                Group = group
            };
        }

        public async Task ApplyRegistrationAsync(long userId, int groupId)
        {
            var groupUsers = await _groupCoordinationService.GetUsersByGroupIdAsync(groupId);
            if (groupUsers.Any() == false)
            {
                await _userService.AttachGroupLeaderRoleAsync(userId);
            }

            await _userService.AttachStudentRoleAsync(userId);
            await _settingRepository.SetValueByKeyAsync(
                userId,
                SettingKeys.GroupId,
                groupId.ToString()
            );
        }
    }
}
