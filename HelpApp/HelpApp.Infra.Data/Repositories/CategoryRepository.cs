using HelpApp.Domain.Interfaces;
using HelpApp.Domain.Entities;
using HelpApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using HelpApp.Domain.Validation;

namespace HelpApp.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetById(int? id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new DomainExceptionValidation("Catehoria não encontrada");
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> Remove(Category category)
        {
            Category categoryToDelete = await GetById(category.Id) ?? throw new DomainExceptionValidation("Erro ao deletar a categoria");
            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return categoryToDelete;
        }

        public async Task<Category> Update(Category category)
        {
            Category categoryDB = await GetById(category.Id) ?? throw new DomainExceptionValidation("Erro ao atualizar a categoria");
            categoryDB!.Name = category.Name;
            _context.Categories.Update(categoryDB);
            await _context.SaveChangesAsync();
            return categoryDB;
        }
    }
}
