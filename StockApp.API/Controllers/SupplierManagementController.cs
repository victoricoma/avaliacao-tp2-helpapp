using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador para gestão de fornecedores
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierManagementController : ControllerBase
    {
        private readonly ISupplierManagementService _supplierManagementService;

        public SupplierManagementController(ISupplierManagementService supplierManagementService)
        {
            _supplierManagementService = supplierManagementService;
        }

        /// <summary>
        /// Adiciona um novo fornecedor
        /// </summary>
        /// <param name="createSupplierDto">Dados do fornecedor a ser criado</param>
        /// <returns>Dados do fornecedor criado</returns>
        /// <response code="201">Fornecedor criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost("suppliers")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<SupplierDTO>> AddSupplier([FromBody] CreateSupplierDto createSupplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _supplierManagementService.AddSupplierAsync(createSupplierDto);
            return CreatedAtAction(nameof(AddSupplier), new { id = result.Id }, result);
        }

        /// <summary>
        /// Gerencia contrato com fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <param name="contractDto">Dados do contrato</param>
        /// <returns>Dados do contrato atualizado</returns>
        /// <response code="200">Contrato gerenciado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpPost("suppliers/{supplierId}/contracts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SupplierContractDto>> ManageContract(int supplierId, [FromBody] SupplierContractDto contractDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _supplierManagementService.ManageContractAsync(supplierId, contractDto);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Avalia um fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <param name="evaluationDto">Dados da avaliação</param>
        /// <returns>Dados da avaliação realizada</returns>
        /// <response code="200">Avaliação realizada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpPost("suppliers/{supplierId}/evaluations")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SupplierEvaluationDto>> EvaluateSupplier(int supplierId, [FromBody] SupplierEvaluationDto evaluationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _supplierManagementService.EvaluateSupplierAsync(supplierId, evaluationDto);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém histórico de avaliações de um fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <returns>Lista de avaliações do fornecedor</returns>
        /// <response code="200">Avaliações obtidas com sucesso</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpGet("suppliers/{supplierId}/evaluations")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<SupplierEvaluationDto>>> GetSupplierEvaluations(int supplierId)
        {
            try
            {
                var result = await _supplierManagementService.GetSupplierEvaluationsAsync(supplierId);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Obtém contratos de um fornecedor
        /// </summary>
        /// <param name="supplierId">ID do fornecedor</param>
        /// <returns>Lista de contratos do fornecedor</returns>
        /// <response code="200">Contratos obtidos com sucesso</response>
        /// <response code="401">Não autorizado</response>
        /// <response code="404">Fornecedor não encontrado</response>
        [HttpGet("suppliers/{supplierId}/contracts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<SupplierContractDto>>> GetSupplierContracts(int supplierId)
        {
            try
            {
                var result = await _supplierManagementService.GetSupplierContractsAsync(supplierId);
                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}