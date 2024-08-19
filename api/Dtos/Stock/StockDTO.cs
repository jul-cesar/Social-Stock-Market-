namespace api.Dtos.Stock;

public record StockDTO(
    int Id,
    string Symbol,
    string CompanyName,
    decimal Purchase,
    decimal LastDiv,
    string Industry,
    long MarketCap
);
