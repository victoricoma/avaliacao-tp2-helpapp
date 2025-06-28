using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet("track-delivery/{trackingNumber}")]
        public async Task<IActionResult> TrackDelivery(string trackingNumber)
        {
            var deliveryInfo = await _deliveryService.GetDeliveryInfoAsync(trackingNumber);
            if (deliveryInfo == null)
            {
                return NotFound("Delivery not found");
            }
            return Ok(deliveryInfo);
        }
    }
}