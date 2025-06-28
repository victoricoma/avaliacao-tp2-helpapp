using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace StockApp.API.Controllers
{
    [Authorize(Policy = "CanManageStock")]
    [Route("/api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        [Authorize(Policy = "CanManageStock")]
        [HttpPost("replenish-stock")]
        public async Task<IActionResult> ReplenishStock()
        {
            await _inventoryService.ReplenishStockAsync();
            return Ok("Reposição automática conluída");
        }
    }
}