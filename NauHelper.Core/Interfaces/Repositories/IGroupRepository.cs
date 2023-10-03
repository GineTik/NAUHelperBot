using NauHelper.Core.Entities;

namespace NauHelper.Core.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetBySpecialtyIdAsync(int specialtyId);
        Task<Group?> GetByIdAsync(int id);
    }
}
