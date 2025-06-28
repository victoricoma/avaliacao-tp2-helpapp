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
        private readonly IJustInTimeInventoryService _justInTimeInventoryService;
        public InventoryController(IInventoryService inventoryService, IJustInTimeInventoryService justInTimeInventoryService)
        {
            _inventoryService = inventoryService;
            _justInTimeInventoryService = justInTimeInventoryService;
        }

        [Authorize(Policy = "CanManageStock")]
        [HttpPost("replenish-stock")]
        public async Task<IActionResult> ReplenishStock()
        {
            await _inventoryService.ReplenishStockAsync();
            return Ok("Reposição automática concluída");
        }

        [HttpPost("optimize")]
        public async Task<IActionResult> OptimizeInventory()
        {
            await _justInTimeInventoryService.OptimizeInventoryAsync();
            return Ok("Inventário otimizado com estratégia Just-in-Time");
        }
    }
}