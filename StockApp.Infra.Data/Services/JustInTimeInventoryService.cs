using System.Threading.Tasks;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System.Linq;

namespace StockApp.Infra.Data.Services
{
   public class JustInTimeInventoryService : IJustInTimeInventoryService
    {
        private readonly IProductRepository _productRepository;

        public JustInTimeInventoryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task OptimizeInventoryAsync()
        {
            var products = await _productRepository.GetAllAsync();

            foreach (var product in products)
            {
                if (product.Stock < product.MinimumStockLevel)
                {
                    int replenishAmount = product.MinimumStockLevel - product.Stock;
                    product.Stock += replenishAmount;

                    await _productRepository.UpdateAsync(product);
                }
            }
        }
    }
}
