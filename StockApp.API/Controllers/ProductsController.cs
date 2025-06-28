using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using StockApp.Domain.Interfaces;
using StockApp.Domain.Entities;


namespace StockApp.API.Controllers;

/// <summary>
/// Controlador responsável pelo gerenciamento de produtos
/// </summary>
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICacheService _cache;
    private readonly IProductRepository _productRepository;
    private readonly IProductService _productService;
    public ProductsController(IProductService productService, IProductRepository productRepository, ICacheService cache)
    {
        _productService = productService;
        _productRepository = productRepository;
        _cache = cache;
    }

    /// <summary>
    /// Obtém todos os produtos
    /// </summary>
    /// <returns>Lista de produtos</returns>
    /// <response code="200">Retorna a lista de produtos</response>
    /// <response code="401">Não autorizado</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        const string cacheKey = "products_all";

        var cachedProducts = await _cache.GetAsync<List<ProductDTO>>(cacheKey);
        if (cachedProducts != null)
        {
            return Ok(cachedProducts);
        }

        var products = await _productService.GetProducts();

        await _cache.SetAsync(cacheKey, products, TimeSpan.FromMinutes(10));

        return Ok(products);
    }

    /// <summary>
    /// Obtém produtos com paginação
    /// </summary>
    /// <param name="paginationParameters">Parâmetros de paginação</param>
    /// <returns>Lista paginada de produtos</returns>
    /// <response code="200">Retorna a lista paginada de produtos</response>
    /// <response code="401">Não autorizado</response>
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<ProductDTO>>> GetAllPaged([FromQuery] PaginationParameters paginationParameters)
    {
        var pagedProducts = await _productService.GetProductsPaged(paginationParameters);
        return Ok(pagedProducts);
    }
 
    /// <summary>
    /// Obtém um produto específico pelo ID
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Produto encontrado</returns>
    /// <response code="200">Retorna o produto encontrado</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="401">Não autorizado</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    /// <summary>
    /// Obtém produtos com estoque baixo
    /// </summary>
    /// <param name="threshold">Limite mínimo de estoque</param>
    /// <returns>Lista de produtos com estoque baixo</returns>
    /// <response code="200">Retorna produtos com estoque baixo</response>
    /// <response code="401">Não autorizado</response>
    [HttpGet("low stock")]
    public async Task<ActionResult<IEnumerable<Product>>> GetLowStock([FromQuery] int threshold)
    {
        var products = await _productRepository.GetLowStockAsync(threshold);
        return Ok(products);
    }

    /// <summary>
    /// Cria um novo produto
    /// </summary>
    /// <param name="productDTO">Dados do produto a ser criado</param>
    /// <returns>Produto criado</returns>
    /// <response code="201">Produto criado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="401">Não autorizado</response>
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO productDTO)
    {
        if (productDTO == null)
        {
            return BadRequest("Invalid Data");
        }
        await _productService.Add(productDTO);
        return CreatedAtAction(nameof(GetById), new { id = productDTO.Id }, productDTO);
    }

    /// <summary>
    /// Atualiza um produto existente
    /// </summary>
    /// <param name="id">ID do produto a ser atualizado</param>
    /// <param name="productDto">Dados atualizados do produto</param>
    /// <returns>Produto atualizado</returns>
    /// <response code="200">Produto atualizado com sucesso</response>
    /// <response code="400">Dados inválidos ou ID não corresponde</response>
    /// <response code="401">Não autorizado</response>
    [HttpPut("{id:int}", Name = "UpdateProduct")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductDTO productDto)
    {
        if (productDto == null || id != productDto.Id)
            return BadRequest();
        await _productService.Update(productDto);
        return Ok(productDto);
    }

    /// <summary>
    /// Remove um produto pelo ID
    /// </summary>
    /// <param name="id">ID do produto</param>
    /// <returns>Produto removido</returns>
    /// <response code="200">Produto removido com sucesso</response>
    /// <response code="404">Produto não encontrado</response>
    /// <response code="401">Não autorizado</response>
    [HttpDelete("{id:int}", Name = "DeleteProduct")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var product = await _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }
        await _productService.Remove(id);
        return Ok(product);
    }
}