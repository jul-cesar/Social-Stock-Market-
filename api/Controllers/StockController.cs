using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository StockRepo;

    public StockController(IStockRepository repo)
    {
        StockRepo = repo;
    }

    [HttpGet, Authorize]
    public async Task<IActionResult> GetAll([FromQuery] StockObjectQuery query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stocks = await StockRepo.GetAllSync(query);
        return Ok(stocks);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = await StockRepo.GetByIdAsync(id);

        if (stock is null)
        {
            return NotFound();
        }
        return Ok(stock);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockDTO newStock)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stock = newStock.ToStock();
        await StockRepo.CreateStockAsync(stock);

        return CreatedAtAction(nameof(GetById), new { Id = stock.Id }, stock.ToDTO());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateStock(
        [FromRoute] int id,
        [FromBody] UpdateStockDTO updatedStock
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockUpdate = await StockRepo.UpdateStockAsync(id, updatedStock);
        if (stockUpdate is null)
        {
            return NotFound();
        }

        return Ok(stockUpdate.ToDTO());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var stockExists = await StockRepo.DeleteStockAsync(id);
        if (stockExists is null)
        {
            return NotFound();
        }

        return NoContent();
    }
}
