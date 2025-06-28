using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{

     public interface ISupplierRepository

    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<(IEnumerable<Supplier> suppliers, int totalCount)> GetSuppliersPaged(int pageNumber, int pageSize);
        Task<Supplier?> GetById(int? id);
        Task<Supplier?> Create(Supplier supplier);
        Task<Supplier?> Update(Supplier supplier);
        Task<Supplier?> Remove(Supplier supplier);
    }
}
