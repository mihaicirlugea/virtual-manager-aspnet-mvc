using Microsoft.EntityFrameworkCore;
using PAW_PROJECT.Data;
using PAW_PROJECT.Models;

namespace PAW_PROJECT.Repositories
{
    public class EcosistemRepository : IEcosistemRepository
    {
        private readonly ApplicationDbContext _context;

        public EcosistemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ecosistem>> GetAllAsync() => await _context.Ecosisteme.ToListAsync();

        public async Task<Ecosistem> GetByIdAsync(int id)
        {
            // Folosim Include pentru a încărca datele din tabelele legate
            return await _context.Ecosisteme
                .Include(e => e.Activitati)
                .Include(e => e.Fluxuri) 
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task AddAsync(Ecosistem ecosistem)
        {
            _context.Ecosisteme.Add(ecosistem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ecosistem ecosistem)
        {
            _context.Ecosisteme.Update(ecosistem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ecosistem = await _context.Ecosisteme.FindAsync(id);
            if (ecosistem != null)
            {
                _context.Ecosisteme.Remove(ecosistem);
                await _context.SaveChangesAsync();
            }
        }
    }
}