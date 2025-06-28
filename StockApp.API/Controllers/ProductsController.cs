using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtos = await _productService.GetProducts();
            if (produtos == null)
            {
                return NotFound("Products not found");
            }
            return Ok(produtos);
        }
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var produto = await _productService.GetProductById(id);
            if (produto == null)
            {
                return NotFound("Product not found");
            }
            return Ok(produto);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
        {
            if (productDTO == null)
                return BadRequest("Data Invalid");

            await _productService.Add(productDTO);

            return new CreatedAtRouteResult("GetProduct",
                new {id = productDTO.Id }, productDTO);
        }
    }
}
