using PAW_PROJECT.Models;

public interface IFluxCapitalService
{
    Task<IEnumerable<FluxCapital>> GetTransactionsAsync();
    Task<FluxCapital> GetTransactionByIdAsync(int id); 
    Task CreateTransactionAsync(FluxCapital flux);
    Task UpdateTransactionAsync(FluxCapital flux); 
    Task DeleteTransactionAsync(int id); 
}