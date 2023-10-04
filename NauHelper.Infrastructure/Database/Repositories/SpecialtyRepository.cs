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

        public async Task AddAsync(int id, string name, int facultyId)
        {
            _dataContext.Specialties.Add(new Specialty
            {
                Id = id,
                Name = name,
                FacultyId = facultyId
            });
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await _dataContext.Specialties.ToListAsync();
        }

        public async Task<IEnumerable<Specialty>> GetByFacultyIdAsync(int facultyId)
        {
            return await _dataContext.Specialties.Where(s => s.FacultyId == facultyId).ToListAsync();
        }

        public async Task<Specialty?> GetByIdAsync(int id)
        {
            return await _dataContext.Specialties.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task RemoveByIdAsync(int id)
        {
            var item = new Specialty { Id = id };
            _dataContext.Specialties.Remove(item);
            await _dataContext.SaveChangesAsync();
        }
    }
}
