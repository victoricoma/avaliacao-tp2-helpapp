using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Infra.Data.Services
{
    //Integração que permite acompanhar o status do pedido diretamente no sistema.//
    public class DeliveryService : IDeliveryService
    {
        private readonly HttpClient _httpClient;

        public DeliveryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DeliveryInfoDTO> GetDeliveryInfoAsync(string trackingNumber)
        {
            var response = await _httpClient.GetAsync($"track/{trackingNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var deliveryInfo = JsonConvert.DeserializeObject<DeliveryInfoDTO>(content);

            return deliveryInfo;
        }
    }
}
