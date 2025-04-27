using HelpApp.Domain.Entities;
using HelpApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using HelpApp.Infra.Data.Context;

namespace HelpApp.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await
            _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetById(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Update (Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> Remove(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
