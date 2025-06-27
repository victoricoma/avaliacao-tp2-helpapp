namespace StockApp.Application.DTOs
{
    public class StockDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}