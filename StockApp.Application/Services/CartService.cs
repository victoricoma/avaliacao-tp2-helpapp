using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
namespace StockApp.Application.Services
{
    public class CartService : ICartService
    {
        private readonly List<CartItem> _cart = new();

        public Task AddToCartAsync(CartItem item)
        {
            var existingItem = _cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;

            }
            else
            {
                _cart.Add(item);
            }
            return Task.CompletedTask;

        }
        public Task<List<CartItem>> GetCardAsync() => Task.FromResult(_cart);
        public Task RemoveFromCartAsync(int productId)
        {
            var item = _cart.FirstOrDefault( c=> c.ProductId == productId);
            if (item != null) _cart.Remove(item);

            return Task.CompletedTask;
        }

        public Task ClearCartAsync()
        {
            _cart.Clear();
            return Task.CompletedTask;
        }

        public Task<List<CartItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoverFromCartAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CartItem>> GetCartAsync()
        {
            throw new NotImplementedException();
        }
    }
}
