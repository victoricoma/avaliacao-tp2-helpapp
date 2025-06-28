using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de categorias
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtém todas as categorias
        /// </summary>
        /// <returns>Lista de categorias</returns>
        /// <response code="200">Retorna a lista de categorias</response>
        /// <response code="404">Categorias não encontradas</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet(Name ="GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get() 
        {
            var categories = await _categoryService.GetCategories();
            if(categories== null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categories);
        }

        /// <summary>
        /// Obtém categorias com paginação
        /// </summary>
        /// <param name="paginationParameters">Parâmetros de paginação</param>
        /// <returns>Lista paginada de categorias</returns>
        /// <response code="200">Retorna a lista paginada de categorias</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetPaged([FromQuery] PaginationParameters paginationParameters)
        {
            var pagedCategories = await _categoryService.GetCategoriesPaged(paginationParameters);
            return Ok(pagedCategories);
        }
        /// <summary>
        /// Obtém uma categoria específica pelo ID
        /// </summary>
        /// <param name="id">ID da categoria</param>
        /// <returns>Categoria encontrada</returns>
        /// <response code="200">Retorna a categoria encontrada</response>
        /// <response code="404">Categoria não encontrada</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if(category== null)
            {
                return NotFound("Category not Found");
            }
            return Ok(category);
        }
        /// <summary>
        /// Cria uma nova categoria
        /// </summary>
        /// <param name="categoryDTO">Dados da categoria a ser criada</param>
        /// <returns>Categoria criada</returns>
        /// <response code="201">Categoria criada com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost(Name ="Create Category")]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if(categoryDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            await _categoryService.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory", 
                new { id = categoryDTO.Id }, categoryDTO);
        }

        /// <summary>
        /// Atualiza uma categoria existente
        /// </summary>
        /// <param name="id">ID da categoria a ser atualizada</param>
        /// <param name="categoryDto">Dados atualizados da categoria</param>
        /// <returns>Categoria atualizada</returns>
        /// <response code="200">Categoria atualizada com sucesso</response>
        /// <response code="400">Dados inválidos ou ID não corresponde</response>
        /// <response code="401">Não autorizado</response>
        [HttpPut("{id:int}", Name = "UpdateCategory")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null || id != categoryDto.Id)
                return BadRequest();
            await _categoryService.Update(categoryDto);
            return Ok(categoryDto);
        }

        /// <summary>
        /// Remove uma categoria
        /// </summary>
        /// <param name="id">ID da categoria a ser removida</param>
        /// <returns>Categoria removida</returns>
        /// <response code="200">Categoria removida com sucesso</response>
        /// <response code="404">Categoria não encontrada</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete("{id:int}", Name ="Delete Category")]
        public async Task<ActionResult<CategoryDTO>> Detele(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if(category == null) 
            {
                return NotFound("Category not found");
            }

            await _categoryService.Remove(id);

            return Ok(category);
        }
    }
}
