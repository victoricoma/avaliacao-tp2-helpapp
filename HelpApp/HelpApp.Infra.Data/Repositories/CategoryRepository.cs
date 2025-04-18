using HelpApp.Domain.Entities;
using HelpApp.Domain.Interfaces.Repositories;
using HelpApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpApp.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly AppçicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetByIdAsync (int id){
            return await _dbContext.Categories.FindAsync(id);
        }
        
        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public void Add(Category category){
            _dbContext.Categories.Add(category);
        }

        public void Remove(Category category){
            _dbContext.Categories.Remove(category);
        }

        public void Update(Category category){
            _dbContext.Categories.Update(category);
        }
    }
}
