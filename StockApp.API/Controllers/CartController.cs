using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController: ControllerBase
    {
     private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItem cartItem)
        {
            await _cartService.AddToCartAsync(cartItem);

            return Ok("Item adicionado ao carrinho");

        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetCartAsync();
            return Ok(cart);

        }
        [HttpDelete("{productId}")]

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoverFromCartAsync(productId);
            return Ok("Item removido");
        }
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync();
            return Ok("Carrinho limpo");
        }
    }
}
