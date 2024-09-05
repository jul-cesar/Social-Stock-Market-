using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Stock;

public record UpdateStockDTO(
    [Required] [MaxLength(30, ErrorMessage = "Symbol cannot be over 30 characters.")] string Symbol,
    [Required]
    [MaxLength(100, ErrorMessage = "Company name cannot be over 100 characters.")]
        string CompanyName,
    [Required] [Range(1, 1000000)] decimal Purchase,
    [Required] [Range(0.001, 100)] decimal LastDiv,
    [MaxLength(30, ErrorMessage = "Industry cannot be over 30 characters.")] string Industry,
    [Range(1, 5000000000)] long MarketCap
);
