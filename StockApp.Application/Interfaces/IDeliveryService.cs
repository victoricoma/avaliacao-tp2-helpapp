using System.Threading.Tasks;
using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IDeliveryService
    {
        Task<DeliveryInfoDTO> GetDeliveryInfoAsync(string trackingNumber);
    }
}