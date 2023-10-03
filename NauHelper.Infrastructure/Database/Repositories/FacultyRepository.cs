using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Infrastructure.Database.EF;

namespace NauHelper.Infrastructure.Database.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly DataContext _dataContext;

        public FacultyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Faculty>> GetAllAsync()
        {
            return await _dataContext.Faculties.ToListAsync();
        }

        public async Task<Faculty?> GetByIdAsync(int id)
        {
            return await _dataContext.Faculties.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
