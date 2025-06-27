using AutoMapper;
using MediatR;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Features.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, StockDTO>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public UpdateStockCommandHandler(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<StockDTO> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var stockExists = await _stockRepository.ExistsAsync(request.Stock.Id);
            
            if (!stockExists)
            {
                throw new KeyNotFoundException($"Stock com ID {request.Stock.Id} não encontrado.");
            }

            var stockToUpdate = _mapper.Map<Stock>(request.Stock);
            stockToUpdate.LastUpdate = DateTime.UtcNow;
            
            await _stockRepository.UpdateAsync(stockToUpdate);
            
            // Buscar o stock atualizado para retornar
            var updatedStock = await _stockRepository.GetByIdAsync(request.Stock.Id);
            return _mapper.Map<StockDTO>(updatedStock);
        }
    }
}