using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<IEnumerable<Specialty>> GetByFacultyIdAsync(int facultyId);
        Task<Specialty?> GetByIdAsync(int id);
        Task AddAsync(int id, string name, int facultyId);
        Task RemoveByIdAsync(int id);
    }
}
