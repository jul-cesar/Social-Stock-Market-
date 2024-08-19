using System;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDTO ToDTO(this Stock stock)
        {
            return new StockDTO(
                stock.Id,
                stock.Symbol,
                stock.CompanyName,
                stock.Purchase,
                stock.LastDiv,
                stock.Industry,
                stock.MarketCap
            );
        }

        public static Stock ToStock(this CreateStockDTO stock)
        {
            return new Stock
            {
                Symbol = stock.Symbol,
                Purchase = stock.Purchase,
                MarketCap = stock.MarketCap,
                CompanyName = stock.CompanyName,
                Industry = stock.Industry,
                LastDiv = stock.LastDiv,
            };
        }
    }
}
