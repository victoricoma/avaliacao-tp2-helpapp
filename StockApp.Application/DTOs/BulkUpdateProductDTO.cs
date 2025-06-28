using System.Collections.Generic;

namespace StockApp.Application.DTOs
{
    public class BulkUpdateProductDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}