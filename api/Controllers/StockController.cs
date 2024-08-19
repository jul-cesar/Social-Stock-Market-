using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository StockRepo;

        public StockController(IStockRepository repo)
        {
            StockRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await StockRepo.GetAllSync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
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
            var stock = newStock.ToStock();
            await StockRepo.CreateStockAsync(stock);

            return CreatedAtAction(nameof(GetById), new { Id = stock.Id }, stock.ToDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock(
            [FromRoute] int id,
            [FromBody] UpdateStockDTO updatedStock
        )
        {
            var stockUpdate = await StockRepo.UpdateStockAsync(id, updatedStock);
            if (stockUpdate is null)
            {
                return NotFound();
            }

            return Ok(stockUpdate.ToDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stockExists = await StockRepo.DeleteStockAsync(id);
            if (stockExists is null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
