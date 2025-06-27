using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetSuppliers();

        Task<SupplierDTO> GetSupplierById(int? id);

        Task Add(SupplierDTO supplierDto);

        Task Update(SupplierDTO supplierDto);

        Task Remove(int? id);
    }
}
