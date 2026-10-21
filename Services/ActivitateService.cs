using PAW_PROJECT.Models;
using PAW_PROJECT.Repositories;

namespace PAW_PROJECT.Services
{
    public class ActivitateService : IActivitateService
    {
        private readonly IActivitateRepository _repo;

        public ActivitateService(IActivitateRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Activitate>> GetTasksAsync() => await _repo.GetAllAsync();

        public async Task<Activitate> GetTaskByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateTaskAsync(Activitate activitate) => await _repo.AddAsync(activitate);

        public async Task UpdateTaskAsync(Activitate activitate) => await _repo.UpdateAsync(activitate);

        public async Task DeleteTaskAsync(int id) => await _repo.DeleteAsync(id);
    }
}