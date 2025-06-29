using System.Threading.Tasks;
using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    //Define o contrato do serviço de rastreamento//
    public interface IDeliveryService
    {
        Task<DeliveryInfoDTO> GetDeliveryInfoAsync(string trackingNumber);
    }
}