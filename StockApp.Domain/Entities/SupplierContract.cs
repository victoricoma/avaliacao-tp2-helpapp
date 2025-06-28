using StockApp.Domain.Validation;
using System;

namespace StockApp.Domain.Entities
{
    /// <summary>
    /// Entidade que representa um contrato de fornecedor
    /// </summary>
    public class SupplierContract
    {
        public int Id { get; private set; }
        public int SupplierId { get; private set; }
        public string ContractNumber { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal Value { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        
        // Propriedade de navegação
        public Supplier Supplier { get; set; }
        
        public SupplierContract(string contractNumber, string description, DateTime startDate, DateTime endDate, decimal value)
        {
            ValidateDomain(contractNumber, description, startDate, endDate, value);
            IsActive = true;
            CreatedAt = DateTime.Now;
        }
        
        public SupplierContract(int id, int supplierId, string contractNumber, string description, 
            DateTime startDate, DateTime endDate, decimal value, bool isActive)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            DomainExceptionValidation.When(supplierId < 0, "Invalid SupplierId value.");
            Id = id;
            SupplierId = supplierId;
            ValidateDomain(contractNumber, description, startDate, endDate, value);
            IsActive = isActive;
            CreatedAt = DateTime.Now;
        }
        
        public void Update(string description, DateTime endDate, decimal value, bool isActive)
        {
            ValidateUpdate(description, endDate, value);
            IsActive = isActive;
            UpdatedAt = DateTime.Now;
        }
        
        private void ValidateDomain(string contractNumber, string description, DateTime startDate, DateTime endDate, decimal value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(contractNumber), "Contract number is required.");
            DomainExceptionValidation.When(contractNumber.Length < 3, "Contract number too short. Minimum 3 characters.");
            
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Description is required.");
            DomainExceptionValidation.When(description.Length < 10, "Description too short. Minimum 10 characters.");
            
            DomainExceptionValidation.When(startDate > endDate, "Start date must be before end date.");
            
            DomainExceptionValidation.When(value < 0, "Value must be greater than or equal to zero.");
            
            ContractNumber = contractNumber;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Value = value;
        }
        
        private void ValidateUpdate(string description, DateTime endDate, decimal value)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description), "Description is required.");
            DomainExceptionValidation.When(description.Length < 10, "Description too short. Minimum 10 characters.");
            
            DomainExceptionValidation.When(StartDate > endDate, "Start date must be before end date.");
            
            DomainExceptionValidation.When(value < 0, "Value must be greater than or equal to zero.");
            
            Description = description;
            EndDate = endDate;
            Value = value;
        }
    }
}