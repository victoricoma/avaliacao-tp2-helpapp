using StockApp.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<(IEnumerable<Product> products, int totalCount)> GetProductsPaged(int pageNumber, int pageSize);

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetById(int? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Remove(Product product);
        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
        Task UpdateAsync(Product product);

    }
}
