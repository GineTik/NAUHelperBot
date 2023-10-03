using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Infrastructure.Database.EF;

namespace NauHelper.Infrastructure.Database.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DataContext _dataContext;

        public GroupRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _dataContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetBySpecialtyIdAsync(int specialtyId)
        {
            return await _dataContext.Groups.Where(g => g.SpecialtyId == specialtyId).ToListAsync();
        }
    }
}
