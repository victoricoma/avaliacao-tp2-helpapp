using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
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

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<CategoryDTO>>> GetPaged([FromQuery] PaginationParameters paginationParameters)
        {
            var pagedCategories = await _categoryService.GetCategoriesPaged(paginationParameters);
            return Ok(pagedCategories);
        }
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

        [HttpPut("{id:int}", Name = "UpdateCategory")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null || id != categoryDto.Id)
                return BadRequest();
            await _categoryService.Update(categoryDto);
            return Ok(categoryDto);
        }

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
