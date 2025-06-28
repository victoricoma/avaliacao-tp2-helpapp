using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    /// <summary>
    /// DTO para fornecedor
    /// </summary>
    public class SupplierDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name is required.")]
        [MinLength(3, ErrorMessage = "The Name must be at least 3 characters long.")]
        [MaxLength(100, ErrorMessage = "The Name can be at most 100 characters long.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Contact Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email format.")]
        public string? ContactEmail { get; set; }

        [Required(ErrorMessage = "The Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid Phone Number format.")]
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// Pontuação média de avaliação do fornecedor
        /// </summary>
        public int? EvaluationScore { get; set; }
        
        /// <summary>
        /// Indica se o fornecedor possui contrato ativo
        /// </summary>
        public bool? HasActiveContract { get; set; }
    }
}
