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

        public async Task AddAsync(string name, int specialtyId)
        {
            _dataContext.Groups.Add(new Group
            {
                Name = name,
                SpecialtyId = specialtyId
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _dataContext.Groups.ToListAsync();
        }

        public async Task<Group?> GetByIdAsync(int id)
        {
            return await _dataContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetBySpecialtyIdAsync(int specialtyId)
        {
            return await _dataContext.Groups.Where(g => g.SpecialtyId == specialtyId).ToListAsync();
        }

        public async Task RemoveByIdAsync(int id)
        {
            var item = new Group { Id = id };
            _dataContext.Groups.Remove(item);
            await _dataContext.SaveChangesAsync();
        }
    }
}
