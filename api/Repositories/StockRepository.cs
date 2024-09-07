using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using api.Utils;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class StockRepository(AppDbContext db) : IStockRepository
{
    AppDbContext _db = db;

    public async Task<List<Stock>> GetAllSync(StockObjectQuery query)
    {
        var stocks = _db.Stock.Include(c => c.Comments).AsQueryable();

        if (!String.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(c => c.CompanyName.Contains(query.CompanyName));
        }

        if (!String.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(c => c.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescending
                    ? stocks.OrderByDescending(c => c.Symbol)
                    : stocks.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _db
            .Stock.Include(c => c.Comments)
            .FirstOrDefaultAsync(stock => stock.Id == id);
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

    public async Task<bool> StockExists(int id)
    {
        return await _db.Stock.AnyAsync(stock => stock.Id == id);
    }

    public async Task<Stock?> GetBySymmbolAsync(string symbol)
    {
        return await _db.Stock.FirstOrDefaultAsync(x => x.Symbol == symbol);
    }
}
