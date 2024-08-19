using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public record UpdateStockDTO(
        string Symbol,
        string CompanyName,
        decimal Purchase,
        decimal LastDiv,
        string Industry,
        long MarketCap
    );
}
