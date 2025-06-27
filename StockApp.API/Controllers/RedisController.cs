using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public RedisController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cacheKey = $"StockApp:{id}";
            var cacheValue = await _cache.GetStringAsync(cacheKey);

            if (cacheValue != null)
            {
             return Ok(Encoding.UTF8.GetString(Convert.FromBase64String(cacheValue)));
            }
            var data = Encoding.UTF8.GetBytes("Hello, Redis!");

            await _cache.SetStringAsync(cacheKey, Convert.ToBase64String(data));

            return Ok(data);
        }
    }
}
