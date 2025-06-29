using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class ProductSearchDTO : PaginationParameters
    {
        public ProductFilterDTO Filters { get; set; } = new ProductFilterDTO();

        public bool IsValid()
        {
            return Filters.IsValidPriceRange() && Filters.IsValidStockRange();
        }

        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (!Filters.IsValidPriceRange())
            {
                errors.Add("Minimum price must be less than or equal to maximum price");
            }

            if (!Filters.IsValidStockRange())
            {
                errors.Add("Minimum stock must be less than or equal to maximum stock");
            }

            return errors;
        }
    }
}