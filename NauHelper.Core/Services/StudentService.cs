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

        public StudentService(ISettingRepository settingRepository, IFacultyRepository facultyRepository, ISpecialtyRepository specialtyRepository, IGroupRepository groupRepository)
        {
            _settingRepository = settingRepository;
            _facultyRepository = facultyRepository;
            _specialtyRepository = specialtyRepository;
            _groupRepository = groupRepository;
        }

        public async Task<StudentInfo> GetStudentInfoAsync(long userId)
        {
            var facultyId = await _settingRepository.GetValueByKeyAsync(
                userId,
                SettingKeys.FacultyId
            );

            var specialtyId = await _settingRepository.GetValueByKeyAsync(
                userId,
                SettingKeys.SpecialtyId
            );

            var groupId = await _settingRepository.GetValueByKeyAsync(
                userId,
                SettingKeys.GroupId
            );

            return new StudentInfo
            {
                Faculty = await _facultyRepository.GetByIdAsync(int.Parse(facultyId)),
                Specialty = await _specialtyRepository.GetByIdAsync(int.Parse(specialtyId)),
                Group = await _groupRepository.GetByIdAsync(int.Parse(groupId))
            };
        }

        public async Task SetStudentInfoAsync(long userId, StudentRegistrationInfo info)
        {
            await _settingRepository.SetValueByKeyAsync(
                info.UserId,
                SettingKeys.FacultyId,
                info.FacultyId.ToString()
            );

            await _settingRepository.SetValueByKeyAsync(
                info.UserId,
                SettingKeys.SpecialtyId,
                info.SpecialtyId.ToString()
            );

            await _settingRepository.SetValueByKeyAsync(
                info.UserId,
                SettingKeys.GroupId,
                info.GroupId.ToString()
            );
        }
    }
}
