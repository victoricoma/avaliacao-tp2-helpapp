using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<(IEnumerable<Category> categories, int totalCount)> GetCategoriesPaged(int pageNumber, int pageSize);

        Task<Category> GetById(int? id);

        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task <Category> Remove(Category category);
    }
}
