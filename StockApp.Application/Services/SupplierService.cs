using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliers()
        {
            var suppliers = await _supplierRepository.GetSuppliers();
            return _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);
        }

        public async Task<PagedResult<SupplierDTO>> GetSuppliersPaged(PaginationParameters paginationParameters)
        {
            var (suppliers, totalCount) = await _supplierRepository.GetSuppliersPaged(
                paginationParameters.PageNumber, 
                paginationParameters.PageSize);

            var supplierDTOs = _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);

            return new PagedResult<SupplierDTO>
            {
                Data = supplierDTOs,
                PageNumber = paginationParameters.PageNumber,
                PageSize = paginationParameters.PageSize,
                TotalRecords = totalCount
            };
        }

        public async Task<SupplierDTO> GetSupplierById(int? id)
        {
            var supplier = await _supplierRepository.GetById(id);
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task Add(SupplierDTO supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.Create(supplier);
        }

        public async Task Update(SupplierDTO supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            await _supplierRepository.Update(supplier);
        }

        public async Task Remove(int? id)
        {
            var supplier = await _supplierRepository.GetById(id);
            if (supplier != null)
            {
                await _supplierRepository.Remove(supplier);
            }
        }
    }
}