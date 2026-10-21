using PAW_PROJECT.Models;

namespace PAW_PROJECT.Repositories
{
    public interface IEcosistemRepository
    {
        Task<IEnumerable<Ecosistem>> GetAllAsync();
        Task<Ecosistem> GetByIdAsync(int id);
        Task AddAsync(Ecosistem ecosistem);
        Task UpdateAsync(Ecosistem ecosistem);
        Task DeleteAsync(int id);
    }
}