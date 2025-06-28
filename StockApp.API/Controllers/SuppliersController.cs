using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de fornecedores
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("/api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// Obtém todos os fornecedores
        /// </summary>
        /// <returns>Lista de fornecedores</returns>
        /// <response code="200">Retorna a lista de fornecedores</response>
        /// <response code="404">Fornecedores não encontrados</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet(Name = "GetSuppliers")]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> Get()
        {
            var suppliers = await _supplierService.GetSuppliers();
            if (suppliers == null)
            {
                return NotFound("Suppliers not found");
            }
            return Ok(suppliers);
        }

        /// <summary>
        /// Obtém fornecedores com paginação
        /// </summary>
        /// <param name="paginationParameters">Parâmetros de paginação</param>
        /// <returns>Lista paginada de fornecedores</returns>
        /// <response code="200">Retorna a lista paginada de fornecedores</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PagedResult<SupplierDTO>>> GetPaged([FromQuery] PaginationParameters paginationParameters)
        {
            var pagedSuppliers = await _supplierService.GetSuppliersPaged(paginationParameters);
            return Ok(pagedSuppliers);
        }

        /// <summary>
        /// Obtém um fornecedor específico pelo ID
        /// </summary>
        /// <param name="id">ID do fornecedor</param>
        /// <returns>Fornecedor encontrado</returns>
        /// <response code="200">Retorna o fornecedor encontrado</response>
        /// <response code="404">Fornecedor não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("{id:int}", Name = "GetSupplier")]
        public async Task<ActionResult<SupplierDTO>> Get(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }
            return Ok(supplier);
        }

        /// <summary>
        /// Cria um novo fornecedor
        /// </summary>
        /// <param name="supplierDTO">Dados do fornecedor a ser criado</param>
        /// <returns>Fornecedor criado</returns>
        /// <response code="201">Fornecedor criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost(Name = "CreateSupplier")]
        public async Task<ActionResult> Post([FromBody] SupplierDTO supplierDTO)
        {
            if (supplierDTO == null)
            {
                return BadRequest("Invalid data");
            }

            await _supplierService.Add(supplierDTO);

            return new CreatedAtRouteResult("GetSupplier",
                new { id = supplierDTO.Id }, supplierDTO);
        }

        /// <summary>
        /// Atualiza um fornecedor existente
        /// </summary>
        /// <param name="id">ID do fornecedor a ser atualizado</param>
        /// <param name="supplierDto">Dados atualizados do fornecedor</param>
        /// <returns>Fornecedor atualizado</returns>
        /// <response code="200">Fornecedor atualizado com sucesso</response>
        /// <response code="400">Dados inválidos ou ID não corresponde</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}", Name = "UpdateSupplier")]
        public async Task<IActionResult> Put(int id, [FromBody] SupplierDTO supplierDto)
        {
            if (supplierDto == null || id != supplierDto.Id)
                return BadRequest();
            await _supplierService.Update(supplierDto);
            return Ok(supplierDto);
        }

        /// <summary>
        /// Remove um fornecedor
        /// </summary>
        /// <param name="id">ID do fornecedor a ser removido</param>
        /// <returns>Fornecedor removido</returns>
        /// <response code="200">Fornecedor removido com sucesso</response>
        /// <response code="404">Fornecedor não encontrado</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}", Name = "DeleteSupplier")]
        public async Task<ActionResult<SupplierDTO>> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }

            await _supplierService.Remove(id);

            return Ok(supplier);
        }
    }
}
