using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        var products = await _productService.GetProducts();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        var product = await _productService.GetProductById(id);
        if(product == null)
        {
        return NotFound();
        }
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO productDTO)
    {
        if(productDTO == null)
        {
            return BadRequest("Invalid Data");
        }
        await _productService.Add(productDTO);
        return CreatedAtAction(nameof(GetById), new { id = productDTO.Id }, productDTO);
    }
    
}