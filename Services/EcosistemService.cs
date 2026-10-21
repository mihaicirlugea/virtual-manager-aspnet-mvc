using PAW_PROJECT.Models;
using PAW_PROJECT.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PAW_PROJECT.Services
{
    public class EcosistemService : IEcosistemService
    {
        private readonly IEcosistemRepository _repository;

        public EcosistemService(IEcosistemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Ecosistem> GetEcosistemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task UpdateEcosistemAsync(Ecosistem ecosistem)
        {
            await _repository.UpdateAsync(ecosistem);
        }


        public async Task<IEnumerable<Ecosistem>> GetAllEcosistemsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateEcosistemAsync(Ecosistem ecosistem)
        {
            // Logica de business
            await _repository.AddAsync(ecosistem);
        }

        public async Task DeleteEcosistemAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}