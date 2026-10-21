using Microsoft.EntityFrameworkCore;
using PAW_PROJECT.Data;
using PAW_PROJECT.Models;

namespace PAW_PROJECT.Repositories
{
    public class FluxCapitalRepository : IFluxCapitalRepository
    {
        private readonly ApplicationDbContext _context;

        public FluxCapitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FluxCapital>> GetAllAsync() =>
            await _context.FluxuriCapitale.Include(f => f.Ecosistem).ToListAsync();

        public async Task<FluxCapital> GetByIdAsync(int id) =>
            await _context.FluxuriCapitale.FindAsync(id);

        public async Task AddAsync(FluxCapital flux)
        {
            _context.FluxuriCapitale.Add(flux);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FluxCapital flux)
        {
            _context.Entry(flux).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flux = await _context.FluxuriCapitale.FindAsync(id);
            if (flux != null)
            {
                _context.FluxuriCapitale.Remove(flux);
                await _context.SaveChangesAsync();
            }
        }
    }
}