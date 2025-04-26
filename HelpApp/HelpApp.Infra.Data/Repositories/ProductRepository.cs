using HelpApp.Domain.Interfaces;
using HelpApp.Domain.Entities;
using HelpApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using HelpApp.Domain.Validation;

namespace HelpApp.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context; 
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product product)
        {
            if (product == null) throw new DomainExceptionValidation("Produto inválido!");

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;           
        }

        public async Task<Product> GetById(int? id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id) ?? throw new DomainExceptionValidation("Produto não encontrado");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Remove(Product product)
        {
            if (product == null) throw new DomainExceptionValidation("Produto inválido para remoção");

            Product productDB = await GetById(product.Id);
            _context.Products.Remove(productDB);
            await _context.SaveChangesAsync();
            return productDB;
        }

        public async Task<Product> Update(Product product)
        {
            if (product == null) throw new DomainExceptionValidation("Produto inválido para edição");
            Product productDB = await GetById(product.Id);

            productDB.Description = product.Description;
            productDB.CategoryId = product.CategoryId;
            productDB.Price = product.Price;
            productDB.Stock = product.Stock;
            productDB.Image = product.Image;

            _context.Products.Update(productDB);
            await _context.SaveChangesAsync();

            return productDB;
        }
    }
}
