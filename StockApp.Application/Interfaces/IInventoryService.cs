using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IInventoryService
    {
        Task ReplenishStockAsync();
    }
}

