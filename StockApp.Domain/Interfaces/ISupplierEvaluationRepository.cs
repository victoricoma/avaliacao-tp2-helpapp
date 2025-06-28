using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    /// <summary>
    /// Interface para repositório de avaliações de fornecedores
    /// </summary>
    public interface ISupplierEvaluationRepository
    {
        /// <summary>
        /// Obtém todas as avaliações
        /// </summary>
        Task<IEnumerable<SupplierEvaluation>> GetAllAsync();
        
        /// <summary>
        /// Obtém avaliações por ID de fornecedor
        /// </summary>
        Task<IEnumerable<SupplierEvaluation>> GetBySupplierId(int supplierId);
        
        /// <summary>
        /// Obtém avaliação por ID
        /// </summary>
        Task<SupplierEvaluation?> GetByIdAsync(int id);
        
        /// <summary>
        /// Cria uma nova avaliação
        /// </summary>
        Task<SupplierEvaluation> CreateAsync(SupplierEvaluation evaluation);
        
        /// <summary>
        /// Atualiza uma avaliação existente
        /// </summary>
        Task<SupplierEvaluation> UpdateAsync(SupplierEvaluation evaluation);
        
        /// <summary>
        /// Remove uma avaliação
        /// </summary>
        Task<bool> DeleteAsync(int id);
        
        /// <summary>
        /// Calcula a pontuação média de avaliação de um fornecedor
        /// </summary>
        Task<int?> GetAverageScoreAsync(int supplierId);
    }
}