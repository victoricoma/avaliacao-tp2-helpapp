using System;

namespace StockApp.Application.DTOs
{
    //  Classe criada para representar os dados que recebo da API de rastreamento.//
    public class DeliveryInfoDTO
    {
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public string CarrierName { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}