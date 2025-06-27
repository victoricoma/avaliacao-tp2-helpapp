using StockApp.Domain.Entities;

namespace StockApp.Application.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(CartItem item);
        Task<List<CartItem>> GetCartAsync();
        Task RemoverFromCartAsync(int productId);
        Task ClearCartAsync();

    }
}
