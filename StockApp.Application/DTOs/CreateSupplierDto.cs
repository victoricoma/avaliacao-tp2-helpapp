using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    /// <summary>
    /// DTO para criação de fornecedor
    /// </summary>
    public class CreateSupplierDto
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [MinLength(3, ErrorMessage = "O Nome deve ter pelo menos 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O Nome pode ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Email de contato é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de Email inválido.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "O Número de telefone é obrigatório.")]
        [Phone(ErrorMessage = "Formato de Número de telefone inválido.")]
        public string PhoneNumber { get; set; }
    }
}