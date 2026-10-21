using PAW_PROJECT.Models;
using PAW_PROJECT.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAW_PROJECT.Services
{
    public class FluxCapitalService : IFluxCapitalService
    {
        private readonly IFluxCapitalRepository _repo;

        public FluxCapitalService(IFluxCapitalRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FluxCapital>> GetTransactionsAsync() => await _repo.GetAllAsync();

        public async Task<FluxCapital> GetTransactionByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateTransactionAsync(FluxCapital flux) => await _repo.AddAsync(flux);

        public async Task UpdateTransactionAsync(FluxCapital flux) => await _repo.UpdateAsync(flux);

        public async Task DeleteTransactionAsync(int id) => await _repo.DeleteAsync(id);
    }
}