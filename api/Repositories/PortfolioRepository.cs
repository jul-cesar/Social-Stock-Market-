using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly AppDbContext _DbContext;

    public PortfolioRepository(AppDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<List<Stock>> GetPortfoliosAsync(string userId)
    {
        return await _DbContext
            .Portfolios.Where(p => p.AppUserId == userId)
            .Select(stock => new Stock
            {
                Id = stock.Stock.Id,
                CompanyName = stock.Stock.CompanyName,
                Symbol = stock.Stock.Symbol,
                Industry = stock.Stock.Industry,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                MarketCap = stock.Stock.MarketCap,
            })
            .ToListAsync();
    }

    public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
    {
        await _DbContext.Portfolios.AddAsync(portfolio);
        await _DbContext.SaveChangesAsync();

        return portfolio;
    }

    public async Task<Portfolio?> DeletePortfolioAsync(string symbol, string userId)
    {
        var portfolio = await _DbContext.Portfolios.FirstOrDefaultAsync(p =>
            p.Stock.Symbol.Equals(symbol) && p.AppUserId == userId
        );

        if (portfolio is null)
        {
            return null;
        }
        _DbContext.Portfolios.Remove(portfolio);
        await _DbContext.SaveChangesAsync();
        return portfolio;
    }
}
