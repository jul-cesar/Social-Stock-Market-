using api.Dtos.Stock;
using api.Models;
using api.Utils;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(StockObjectQuery query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateStockAsync(Stock stock);

    Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO stock);

    Task<Stock?> DeleteStockAsync(int id);

    Task<Stock?> GetBySymmbolAsync(string symbol);

    Task<bool> StockExists(int id);
}
