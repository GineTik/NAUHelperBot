using NauHelper.Core.Entities;
using NauHelper.Core.Enums;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Core.Interfaces.Services;

namespace NauHelper.Core.Services
{
    public class StudentGroupCoordinationService : IStudentGroupCoordinationService
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly ISpecialtyRepository _specialtyRepository; 
        private readonly IGroupRepository _groupRepository;
        private readonly IRoleService _roleService;

        public StudentGroupCoordinationService(
            IFacultyRepository facultyRepository,
            ISpecialtyRepository specialtyRepository,
            IGroupRepository groupRepository,
            IRoleService roleService)
        {
            _facultyRepository = facultyRepository;
            _specialtyRepository = specialtyRepository;
            _groupRepository = groupRepository;
            _roleService = roleService;
        }

        public async Task AddFacultyAsync(long plenipotentiaryUserId, string name)
        {
            await throwIfWrongRole(plenipotentiaryUserId);
            await _facultyRepository.AddAsync(name);
        }

        public async Task AddGroupAsync(long plenipotentiaryUserId, string name, int specialtyId)
        {
            await throwIfWrongRole(plenipotentiaryUserId);
            await _groupRepository.AddAsync(name, specialtyId);
        }

        public async Task AddSpecialtyAsync(long plenipotentiaryUserId, int id, string name, int facultyId)
        {
            await throwIfWrongRole(plenipotentiaryUserId);

            var codeLength = id.ToString().Length;
            if (codeLength < 10 && codeLength > 3)
            {
                throw new ArgumentException("Code can be have 3 symbols, for example: 000");
            }

            var specialties = await GetAllSpecialtiesAsync();
            if (specialties.Any(s => s.Id == id) == true)
            {
                throw new ArgumentException("Code already exists");
            }

            await _specialtyRepository.AddAsync(id, name, facultyId);
        }

        public Task<IEnumerable<Faculty>> GetAllFacultiesAsync()
        {
            return _facultyRepository.GetAllAsync();
        }

        public Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return _groupRepository.GetAllAsync();
        }

        public Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync()
        {
            return _specialtyRepository.GetAllAsync();
        }

        public async Task RemoveFacultyAsync(long plenipotentiaryUserId, int id)
        {
            await removeAsync(_facultyRepository.RemoveByIdAsync, plenipotentiaryUserId, id);
        }

        public async Task RemoveGroupAsync(long plenipotentiaryUserId, int id)
        {
            await removeAsync(_groupRepository.RemoveByIdAsync, plenipotentiaryUserId, id);
        }

        public async Task RemoveSpecialtyAsync(long plenipotentiaryUserId, int id)
        {
            await removeAsync(_specialtyRepository.RemoveByIdAsync, plenipotentiaryUserId, id);
        }

        private async Task throwIfWrongRole(long plenipotentiaryUserId)
        {
            if (await _roleService.HaveRoleAsync(plenipotentiaryUserId, (int)ExistingRoles.Administrator) == false)
            {
                throw new InvalidOperationException("User haven't administrator role");
            }
        }

        private async Task removeAsync(Func<int, Task> method, long plenipotentiaryUserId, int id)
        {
            await throwIfWrongRole(plenipotentiaryUserId);
            await method(id);
        }
    }
}
