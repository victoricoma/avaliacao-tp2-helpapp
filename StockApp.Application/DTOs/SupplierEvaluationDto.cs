using System;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Application.DTOs
{
    /// <summary>
    /// DTO para avaliação de fornecedor
    /// </summary>
    public class SupplierEvaluationDto
    {
        public int Id { get; set; }
        
        public int SupplierId { get; set; }
        
        [Required(ErrorMessage = "A Categoria de avaliação é obrigatória.")]
        public string Category { get; set; }
        
        [Required(ErrorMessage = "O Comentário é obrigatório.")]
        [MinLength(10, ErrorMessage = "O Comentário deve ter pelo menos 10 caracteres.")]
        public string Comment { get; set; }
        
        [Range(0, 100, ErrorMessage = "A Pontuação deve estar entre 0 e 100.")]
        public int Score { get; set; }
        
        [Required(ErrorMessage = "O Avaliador é obrigatório.")]
        public string EvaluatedBy { get; set; }
        
        public DateTime EvaluationDate { get; set; } = DateTime.Now;
    }
}