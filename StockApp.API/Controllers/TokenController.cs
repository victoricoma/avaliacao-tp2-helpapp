using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Infra.Data.Identity;
using StockApp.Application;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;
        public TokenController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
         
            var token = await _authService.AuthenticateAsync(userLoginDTO.Username, userLoginDTO.Password);
            if (token == null)
            {
                return Unauthorized("Invalid username or password");
            }
            return Ok(token);
        }
    }
}
