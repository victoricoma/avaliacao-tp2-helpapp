using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        public InventoryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public  async Task ReplenishStockAsync()
        {
            var lowStockProducts = await _productRepository.GetLowStockAsync(10);
            foreach (var product in lowStockProducts)
            {
                product.Stock += 50;
                await _productRepository.Update(product);
            }
        }
    }
}

