using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class ReportDTO
    {
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string? GeneratedBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class ProductReportDTO : ReportDTO
    {
        public ProductReportSummaryDTO Summary { get; set; } = new();
        public ProductFilterDTO Filters { get; set; } = new();
        public PaginationInfoDTO Pagination { get; set; } = new();
        public IEnumerable<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

    public class ProductReportSummaryDTO
    {
        public int TotalProducts { get; set; }
        public decimal TotalValue { get; set; }
        public decimal AveragePrice { get; set; }
        public int LowStockCount { get; set; }
        public int FilteredResults { get; set; }
        public decimal? HighestPrice { get; set; }
        public decimal? LowestPrice { get; set; }
        public int TotalStock { get; set; }
    }

    public class PaginationInfoDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

    public class LowStockReportDTO : ReportDTO
    {
        public int Threshold { get; set; }
        public int TotalLowStockProducts { get; set; }
        public decimal TotalValue { get; set; }
        public IEnumerable<LowStockProductDTO> Products { get; set; } = new List<LowStockProductDTO>();
    }

    public class LowStockProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal Value => Price * Stock;
        public string? Category { get; set; }
        public string StockStatus { get; set; } = string.Empty;
        public int? MinimumStockLevel { get; set; }
        public int StockDeficit => MinimumStockLevel.HasValue ? Math.Max(0, MinimumStockLevel.Value - Stock) : 0;
    }

    public class CategoryReportDTO : ReportDTO
    {
        public CategoryReportSummaryDTO Summary { get; set; } = new();
        public IEnumerable<CategoryStatsDTO> Categories { get; set; } = new List<CategoryStatsDTO>();
    }

    public class CategoryReportSummaryDTO
    {
        public int TotalCategories { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalValue { get; set; }
    }

    public class CategoryStatsDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int ProductCount { get; set; }
        public decimal TotalValue { get; set; }
        public decimal AveragePrice { get; set; }
        public int TotalStock { get; set; }
        public decimal PercentageOfTotal { get; set; }
    }
}