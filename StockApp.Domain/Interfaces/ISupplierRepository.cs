using StockApp.Domain.Entities;
namespace StockApp.Domain.Interfaces
{
     public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier?> GetById(int? id);
        Task<Supplier?> Create(Supplier supplier);
        Task<Supplier?> Update(Supplier supplier);
        Task<Supplier?> Remove(Supplier supplier);       
        
    }
}
