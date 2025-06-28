using System;

namespace StockApp.Application.DTOs
{
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