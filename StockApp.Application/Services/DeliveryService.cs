using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly HttpClient _httpClient;

        public DeliveryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DeliveryInfoDTO> GetDeliveryInfoAsync(string trackingNumber)
        {
            await Task.Delay(100);


            return new DeliveryInfoDTO
            {
                TrackingNumber = trackingNumber,
                Status = "Em trânsito",
                CurrentLocation = "São Paulo - SP",
                EstimatedDeliveryDate = DateTime.Now.AddDays(2),
                CarrierName = "Correios",
                LastUpdated = DateTime.Now
            };
        }
    }
}