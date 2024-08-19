using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllSync();
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateStockAsync(Stock stock);  

        Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO stock);  

        Task<Stock?> DeleteStockAsync(int id);  
    }
}
