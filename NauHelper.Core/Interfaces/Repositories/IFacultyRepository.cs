using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface IFacultyRepository
    {
        Task<IEnumerable<Faculty>> GetAllAsync();
        Task<Faculty?> GetByIdAsync(int id);
    }
}
