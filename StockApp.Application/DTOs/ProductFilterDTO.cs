using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class ProductFilterDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum price must be greater than or equal to 0")]
        public decimal? MinPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum price must be greater than or equal to 0")]
        public decimal? MaxPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Minimum stock must be greater than or equal to 0")]
        public int? MinStock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Maximum stock must be greater than or equal to 0")]
        public int? MaxStock { get; set; }

        public bool? IsLowStock { get; set; }

        public string? SortBy { get; set; }

        public string? SortDirection { get; set; } = "asc";

        public bool IsValidPriceRange()
        {
            if (MinPrice.HasValue && MaxPrice.HasValue)
            {
                return MinPrice.Value <= MaxPrice.Value;
            }
            return true;
        }

        public bool IsValidStockRange()
        {
            if (MinStock.HasValue && MaxStock.HasValue)
            {
                return MinStock.Value <= MaxStock.Value;
            }
            return true;
        }
    }
}