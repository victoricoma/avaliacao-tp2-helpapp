using MediatR;
using StockApp.Application.DTOs;

namespace StockApp.Application.Features.Stocks.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest<StockDTO>
    {
        public UpdateStockDTO Stock { get; set; }

        public UpdateStockCommand(UpdateStockDTO stock)
        {
            Stock = stock;
        }
    }
}