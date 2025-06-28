using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.Infra.Data.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _context.Suppliers.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<(IEnumerable<Supplier> suppliers, int totalCount)> GetSuppliersPaged(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Suppliers.CountAsync();
            
            var suppliers = await _context.Suppliers
                .OrderBy(s => s.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (suppliers, totalCount);
        }

        public async Task<Supplier> GetById(int? id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<Supplier> Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier> Remove(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }
    }
}
