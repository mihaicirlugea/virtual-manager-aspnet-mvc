using PAW_PROJECT.Models;

namespace PAW_PROJECT.Repositories
{
    public interface IFluxCapitalRepository
    {
        Task<IEnumerable<FluxCapital>> GetAllAsync();
        Task<FluxCapital> GetByIdAsync(int id);
        Task AddAsync(FluxCapital flux);
        Task UpdateAsync(FluxCapital flux);
        Task DeleteAsync(int id);
    }
}