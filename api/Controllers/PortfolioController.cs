using System.Security.Claims;
using api.Interfaces;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("/api/portfolio")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioRepository PortfolioRepo;
    private readonly IStockRepository StockRepo;

    public PortfolioController(
        IPortfolioRepository portfolioRepository,
        IStockRepository stockRepository
    )
    {
        PortfolioRepo = portfolioRepository;
        StockRepo = stockRepository;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetUserPortfolios()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var portfolios = await PortfolioRepo.GetPortfoliosAsync(userId);

        if (portfolios is null)
        {
            return NotFound();
        }

        return Ok(portfolios);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortfolio(string symbol)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized("User is not authenticated.");
        }
        var stock = await StockRepo.GetBySymmbolAsync(symbol);
        if (stock is null)
        {
            return NotFound("Could not find a stock with the given symbol.");
        }

        var portfolio = new Portfolio { AppUserId = userId, StockId = stock.Id };
        var userPorfs = await PortfolioRepo.GetPortfoliosAsync(userId);
        if (userPorfs.Any(p => p.Symbol.Equals(symbol, StringComparison.CurrentCultureIgnoreCase)))
        {
            return BadRequest("Stock already on portfolio");
        }
        await PortfolioRepo.CreatePortfolioAsync(portfolio);
        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized("User is not authenticated.");
        }

        var userPortfolios = await PortfolioRepo.GetPortfoliosAsync(userId);

        var filteredStocks = userPortfolios.Where(p => p.Symbol.Equals(symbol));

        if (filteredStocks.Count() == 1)
        {
            await PortfolioRepo.DeletePortfolioAsync(symbol, userId);
        }
        else
        {
            return BadRequest("Stock not in your portfolio");
        }

        return Ok();
    }
}
