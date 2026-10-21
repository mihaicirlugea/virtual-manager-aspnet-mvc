using Microsoft.EntityFrameworkCore;
using PAW_PROJECT.Data;
using PAW_PROJECT.Models;

namespace PAW_PROJECT.Repositories
{
    public class ActivitateRepository : IActivitateRepository
    {
        private readonly ApplicationDbContext _context;
        public ActivitateRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Activitate>> GetAllAsync() =>
            await _context.Activitati.Include(a => a.Ecosistem).ToListAsync();

        public async Task<Activitate> GetByIdAsync(int id) =>
            await _context.Activitati.Include(a => a.Ecosistem).FirstOrDefaultAsync(m => m.Id == id);

        public async Task AddAsync(Activitate activitate)
        {
            _context.Activitati.Add(activitate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Activitate activitate)
        {
            _context.Entry(activitate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var activitate = await _context.Activitati.FindAsync(id);
            if (activitate != null)
            {
                _context.Activitati.Remove(activitate);
                await _context.SaveChangesAsync();
            }
        }
    }
}