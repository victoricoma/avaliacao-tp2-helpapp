using System;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    /// <summary>
    /// DTO para contrato de fornecedor
    /// </summary>
    public class SupplierContractDto
    {
        public int Id { get; set; }
        
        public int SupplierId { get; set; }
        
        [Required(ErrorMessage = "O Número do contrato é obrigatório.")]
        public string ContractNumber { get; set; }
        
        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        [MinLength(10, ErrorMessage = "A Descrição deve ter pelo menos 10 caracteres.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "A Data de início é obrigatória.")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "A Data de término é obrigatória.")]
        public DateTime EndDate { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "O Valor deve ser maior ou igual a zero.")]
        public decimal Value { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
    }
}