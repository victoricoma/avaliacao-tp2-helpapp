using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    public class UpdateStockDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O símbolo é obrigatório")]
        [StringLength(10, ErrorMessage = "O símbolo deve ter no máximo 10 caracteres")]
        public string Symbol { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal CurrentPrice { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
        public int Quantity { get; set; }
    }
}