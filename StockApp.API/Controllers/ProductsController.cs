using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
    }
}
