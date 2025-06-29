using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.Create(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsEntity = await _productRepository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<PagedResult<ProductDTO>> GetProductsPaged(PaginationParameters paginationParameters)
        {
            var (products, totalCount) = await _productRepository.GetProductsPaged(
                paginationParameters.PageNumber,
                paginationParameters.PageSize);

            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return new PagedResult<ProductDTO>(
                productDTOs,
                paginationParameters.PageNumber,
                paginationParameters.PageSize,
                totalCount);
        }

        public async Task<PagedResult<ProductDTO>> GetProductsWithFiltersAsync(ProductSearchDTO searchParameters)
        {
            var filters = searchParameters.Filters;
            
            var (products, totalCount) = await _productRepository.GetProductsWithFiltersAsync(
                searchParameters.PageNumber,
                searchParameters.PageSize,
                filters.Name,
                filters.Description,
                filters.CategoryId,
                filters.MinPrice,
                filters.MaxPrice,
                filters.MinStock,
                filters.MaxStock,
                filters.IsLowStock,
                filters.SortBy,
                filters.SortDirection);
            
            var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
            
            return new PagedResult<ProductDTO>(
                productDTOs, 
                searchParameters.PageNumber, 
                searchParameters.PageSize, 
                totalCount);
        }

        public async Task<ProductDTO> GetProductById(int? id)
        {
            var productEntity = _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task Remove(int? id)
        {
            var productEntity = _productRepository.GetById(id).Result;
            await _productRepository.Remove(productEntity);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.Update(productEntity);
        }
    }
}
