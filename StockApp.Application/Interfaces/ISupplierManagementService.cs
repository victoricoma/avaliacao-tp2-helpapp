using StockApp.Application.DTOs;
using System.Threading.Tasks;


namespace StockApp.Application.Interfaces
{
    /// <summary>
    /// Interface para o serviço de gestão de fornecedores
    /// </summary>
    public interface ISupplierManagementService
    {
        /// <summary>
        /// Adiciona um novo fornecedor
        /// </summary>
        /// <param name="createSupplierDto">Dados do fornecedor a ser criado</param>
        /// <returns>Dados do fornecedor criado</returns>
        Task<SupplierDTO> AddSupplierAsync(CreateSupplierDto createSupplierDto);

        /// <summary>
        /// Gerencia contrato com fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <param name="contractDto">Dados do contrato</param>
        /// <returns>Dados do contrato atualizado</returns>
        Task<SupplierContractDto> ManageContractAsync(int supplierId, SupplierContractDto contractDto);

        /// <summary>
        /// Avalia um fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <param name="evaluationDto">Dados da avaliação</param>
        /// <returns>Dados da avaliação realizada</returns>
        Task<SupplierEvaluationDto> EvaluateSupplierAsync(int supplierId, SupplierEvaluationDto evaluationDto);

        /// <summary>
        /// Obtém histórico de avaliações de um fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <returns>Lista de avaliações do fornecedor</returns>
        Task<IEnumerable<SupplierEvaluationDto>> GetSupplierEvaluationsAsync(int supplierId);

        /// <summary>
        /// Obtém contratos de um fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <returns>Lista de contratos do fornecedor</returns>
        Task<IEnumerable<SupplierContractDto>> GetSupplierContractsAsync(int supplierId);
    }
}