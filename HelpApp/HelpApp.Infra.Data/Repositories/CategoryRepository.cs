using HelpApp.Domain.Entities;
using HelpApp.Domain.Interfaces;
using HelpApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HelpApp.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetById(int? id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> Create(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            return category;
        }

        public async Task<Category> Remove(Category category)
        {
            _dbContext.Categories.Remove(category);
            return category;

        }

        public async Task<Category> Update(Category category)
        {
            _dbContext.Categories.Update(category);
            return category;
        }

    }
}
