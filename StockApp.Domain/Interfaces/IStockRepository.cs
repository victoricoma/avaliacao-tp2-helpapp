using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByIdAsync(int id);
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}