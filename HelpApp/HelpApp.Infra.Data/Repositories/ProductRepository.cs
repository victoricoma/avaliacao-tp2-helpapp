using HelpApp.Domain.Entities;
using HelpApp.Domain.Interfaces;
using HelpApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpApp.Infra.Data.Repositories
{

    public class ProductRepository : IProductRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetById(int? id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> Create(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            return product;
        }

        public async Task<Product> Remove(Product product)
        {
            _dbContext.Products.Remove(product);
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            return product;
        }
    }
}
