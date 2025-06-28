using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("replenish-stock")]
        public async Task<IActionResult> ReplenishStock()
        {
            await _inventoryService.ReplenishStockAsync();
            return Ok("Reposição automática conluída");
        }
    }
}