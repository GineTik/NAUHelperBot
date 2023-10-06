using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Services
{
    public interface IStudentGroupCoordinationService
    {
        Task<IEnumerable<Faculty>> GetAllFacultiesAsync();
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<User>> GetUsersByGroupIdAsync(int groupId);

        Task AddFacultyAsync(long plenipotentiaryUserId, string name);
        Task AddSpecialtyAsync(long plenipotentiaryUserId, int id, string name, int facultyId);
        Task AddGroupAsync(long plenipotentiaryUserId, string name, int specialtyId);

        Task RemoveFacultyAsync(long plenipotentiaryUserId, int id);
        Task RemoveSpecialtyAsync(long plenipotetiaryUserId, int id);
        Task RemoveGroupAsync(long plenipotetiaryUserId, int id);

    }
}
