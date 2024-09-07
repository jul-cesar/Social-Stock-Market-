using api.Models;

namespace api.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetPortfoliosAsync(string userId);

    Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);

    Task<Portfolio?> DeletePortfolioAsync(string symbol, string userId);
}
