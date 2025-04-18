using HelpApp.Domain.Entities;
using HelpApp.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using HelpApp.Infrastructure.Data;

namespace HelpApp.Infra.Data.Repositories
{

    public class ProductRepository : IProductRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetByIdAsync (int id){
            return await _dbContext.Products.FindAsync(id);
        }
        
        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public void Add(Product product){
            _dbContext.Products.Add(product);
        }

        public void Remove(Product product){
            _dbContext.Products.Remove(product);
        }

        public void Update(Product product){
            _dbContext.Entry(product).State = EntityState.Modified;
        }
    }
}
