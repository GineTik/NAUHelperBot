using Microsoft.EntityFrameworkCore;
using NauHelper.Core.Entities;
using NauHelper.Core.Interfaces.Repositories;
using NauHelper.Infrastructure.Database.EF;

namespace NauHelper.Infrastructure.Database.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly DataContext _dataContext;

        public SpecialtyRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Specialty>> GetByFacultyIdAsync(int facultyId)
        {
            return await _dataContext.Specialties.Where(s => s.FacultyId == facultyId).ToListAsync();
        }

        public async Task<Specialty?> GetByIdAsync(int id)
        {
            return await _dataContext.Specialties.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
