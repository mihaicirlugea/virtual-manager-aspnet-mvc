using PAW_PROJECT.Models;

namespace PAW_PROJECT.Services
{
    public interface IActivitateService
    {
        Task<IEnumerable<Activitate>> GetTasksAsync();
        Task<Activitate> GetTaskByIdAsync(int id);
        Task CreateTaskAsync(Activitate activitate);
        Task UpdateTaskAsync(Activitate activitate);
        Task DeleteTaskAsync(int id);
    }
}