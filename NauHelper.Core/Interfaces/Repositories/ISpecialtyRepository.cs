using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetByFacultyIdAsync(int facultyId);
        Task<Specialty?> GetByIdAsync(int id);
    }
}
