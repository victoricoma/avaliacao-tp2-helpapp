using StockApp.Domain.Validation;
using System;

namespace StockApp.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma avaliação de fornecedor
    /// </summary>
    public class SupplierEvaluation
    {
        public int Id { get; private set; }
        public int SupplierId { get; private set; }
        public string Category { get; private set; }
        public string Comment { get; private set; }
        public int Score { get; private set; }
        public string EvaluatedBy { get; private set; }
        public DateTime EvaluationDate { get; private set; }
        
        // Propriedade de navegação
        public Supplier Supplier { get; set; }
        
        public SupplierEvaluation(string category, string comment, int score, string evaluatedBy)
        {
            ValidateDomain(category, comment, score, evaluatedBy);
            EvaluationDate = DateTime.Now;
        }
        
        public SupplierEvaluation(int id, int supplierId, string category, string comment, int score, string evaluatedBy, DateTime evaluationDate)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(supplierId < 0, "Invalid SupplierId value.");
            Id = id;
            SupplierId = supplierId;
            ValidateDomain(category, comment, score, evaluatedBy);
            EvaluationDate = evaluationDate;
        }
        
        private void ValidateDomain(string category, string comment, int score, string evaluatedBy)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(category), "Category is required.");
            
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(comment), "Comment is required.");
            DomainExceptionValidation.When(comment.Length < 10, "Comment too short. Minimum 10 characters.");
            
            DomainExceptionValidation.When(score < 0 || score > 100, "Score must be between 0 and 100.");
            
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(evaluatedBy), "Evaluator is required.");
            
            Category = category;
            Comment = comment;
            Score = score;
            EvaluatedBy = evaluatedBy;
        }
    }
}