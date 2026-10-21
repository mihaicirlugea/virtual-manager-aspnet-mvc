using PAW_PROJECT.Models;

namespace PAW_PROJECT.Services
{
    public interface IEcosistemService
    {
        Task<IEnumerable<Ecosistem>> GetAllEcosistemsAsync();
        Task CreateEcosistemAsync(Ecosistem ecosistem);
        Task DeleteEcosistemAsync(int id);
        Task UpdateEcosistemAsync(Ecosistem ecosistem);
        Task<Ecosistem> GetEcosistemByIdAsync(int id);

    }
}