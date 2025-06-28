using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo rastreamento de entregas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        /// <summary>
        /// Rastreia uma entrega pelo número de rastreamento
        /// </summary>
        /// <param name="trackingNumber">Número de rastreamento da entrega</param>
        /// <returns>Informações da entrega</returns>
        /// <response code="200">Retorna as informações da entrega</response>
        /// <response code="404">Entrega não encontrada</response>
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