using PAW_PROJECT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAW_PROJECT.Repositories
{
    public interface IActivitateRepository
    {
        Task<IEnumerable<Activitate>> GetAllAsync();
        Task<Activitate> GetByIdAsync(int id);
        Task AddAsync(Activitate activitate);
        Task UpdateAsync(Activitate activitate); 
        Task DeleteAsync(int id); 
    }
}