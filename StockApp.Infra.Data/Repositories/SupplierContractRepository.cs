using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Repositories
{
    /// <summary>
    /// Repositório para contratos de fornecedores
    /// </summary>
    public class SupplierContractRepository : ISupplierContractRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierContractRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierContract>> GetAllAsync()
        {
            return await _context.SupplierContracts
                .Include(c => c.Supplier)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<SupplierContract>> GetBySupplierId(int supplierId)
        {
            return await _context.SupplierContracts
                .Where(c => c.SupplierId == supplierId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<SupplierContract?> GetByIdAsync(int id)
        {
            return await _context.SupplierContracts
                .Include(c => c.Supplier)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SupplierContract> CreateAsync(SupplierContract contract)
        {
            _context.SupplierContracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<SupplierContract> UpdateAsync(SupplierContract contract)
        {
            _context.SupplierContracts.Update(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contract = await GetByIdAsync(id);
            if (contract == null)
                return false;

            _context.SupplierContracts.Remove(contract);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasActiveContractAsync(int supplierId)
        {
            return await _context.SupplierContracts
                .AnyAsync(c => c.SupplierId == supplierId && c.IsActive && c.EndDate >= DateTime.Now);
        }
    }
}