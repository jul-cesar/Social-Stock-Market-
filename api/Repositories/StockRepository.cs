using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        AppDbContext _db;

        public StockRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Stock>> GetAllSync()
        {
            return await _db.Stock.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _db.Stock.FirstAsync(x => x.Id == id);
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            await _db.Stock.AddAsync(stock);
            await _db.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDTO stock)
        {
            var stockExists = await _db.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if (stockExists is null)
            {
                return null;
            }

            stockExists.Symbol = stock.Symbol;
            stockExists.Purchase = stock.Purchase;
            stockExists.MarketCap = stock.MarketCap;
            stockExists.Industry = stock.Industry;
            stockExists.LastDiv = stock.LastDiv;

            await _db.SaveChangesAsync();
            return stockExists;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var deletedStock = await _db.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
            if (deletedStock is null)
            {
                return null;
            }

            _db.Remove(deletedStock);
            return deletedStock;
        }
    }
}
