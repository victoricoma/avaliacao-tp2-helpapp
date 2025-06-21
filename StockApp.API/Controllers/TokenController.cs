using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            if(loginDto.UserName == "testuser321" && loginDto.Password == "password123")
            {
                return Ok(new TokenResponseDto
                {
                    Token = "fake-jwt-token",
                    Expiration = DateTime.UtcNow.AddHours(1)
                });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
