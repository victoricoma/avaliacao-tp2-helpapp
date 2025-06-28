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
    /// Repositório para avaliações de fornecedores
    /// </summary>
    public class SupplierEvaluationRepository : ISupplierEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplierEvaluation>> GetAllAsync()
        {
            return await _context.SupplierEvaluations
                .Include(e => e.Supplier)
                .OrderByDescending(e => e.EvaluationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<SupplierEvaluation>> GetBySupplierId(int supplierId)
        {
            return await _context.SupplierEvaluations
                .Where(e => e.SupplierId == supplierId)
                .OrderByDescending(e => e.EvaluationDate)
                .ToListAsync();
        }

        public async Task<SupplierEvaluation?> GetByIdAsync(int id)
        {
            return await _context.SupplierEvaluations
                .Include(e => e.Supplier)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<SupplierEvaluation> CreateAsync(SupplierEvaluation evaluation)
        {
            _context.SupplierEvaluations.Add(evaluation);
            await _context.SaveChangesAsync();
            return evaluation;
        }

        public async Task<SupplierEvaluation> UpdateAsync(SupplierEvaluation evaluation)
        {
            _context.SupplierEvaluations.Update(evaluation);
            await _context.SaveChangesAsync();
            return evaluation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evaluation = await GetByIdAsync(id);
            if (evaluation == null)
                return false;

            _context.SupplierEvaluations.Remove(evaluation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> GetAverageScoreAsync(int supplierId)
        {
            var evaluations = await _context.SupplierEvaluations
                .Where(e => e.SupplierId == supplierId)
                .ToListAsync();

            if (!evaluations.Any())
                return null;

            return (int)evaluations.Average(e => e.Score);
        }
    }
}