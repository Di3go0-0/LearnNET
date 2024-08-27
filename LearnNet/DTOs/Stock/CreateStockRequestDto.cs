using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Company name must be at most 50 characters long")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,1000, ErrorMessage = "Purchase price must be between 1 and 1000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "Last dividend must be between 0.001 and 100")]
        public decimal LastDiv { get; set; }
        [Required] 
        [MaxLength(10, ErrorMessage = "Industry must be at most 10 characters long")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1,5000000000, ErrorMessage = "Market cap must be between 1 and 5000000000")]
        public long MarketCap { get; set; }
    }
}