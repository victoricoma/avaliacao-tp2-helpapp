using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetById(int id);
        Task<IEnumerable<Stock>> GetAll();
        Task<Stock> Add(Stock stock);
        Task Update(Stock stock);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}