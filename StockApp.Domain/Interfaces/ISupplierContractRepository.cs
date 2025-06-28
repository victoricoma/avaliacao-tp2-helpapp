using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    /// <summary>
    /// Interface para repositório de contratos de fornecedores
    /// </summary>
    public interface ISupplierContractRepository
    {
        /// <summary>
        /// Obtém todos os contratos
        /// </summary>
        Task<IEnumerable<SupplierContract>> GetAllAsync();
        
        /// <summary>
        /// Obtém contratos por ID de fornecedor
        /// </summary>
        Task<IEnumerable<SupplierContract>> GetBySupplierId(int supplierId);
        
        /// <summary>
        /// Obtém contrato por ID
        /// </summary>
        Task<SupplierContract?> GetByIdAsync(int id);
        
        /// <summary>
        /// Cria um novo contrato
        /// </summary>
        Task<SupplierContract> CreateAsync(SupplierContract contract);
        
        /// <summary>
        /// Atualiza um contrato existente
        /// </summary>
        Task<SupplierContract> UpdateAsync(SupplierContract contract);
        
        /// <summary>
        /// Remove um contrato
        /// </summary>
        Task<bool> DeleteAsync(int id);
        
        /// <summary>
        /// Verifica se um fornecedor possui contrato ativo
        /// </summary>
        Task<bool> HasActiveContractAsync(int supplierId);
    }
}