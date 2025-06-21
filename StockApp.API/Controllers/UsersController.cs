using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase 
    {
        private static readonly List<string> RegisteredUsers = new();

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest("Password is required");

                if (RegisteredUsers.Contains(userDto.UserName))
                    return Conflict("Username already exists");

                RegisteredUsers.Add(userDto.UserName);

                return Ok(new { message = "User registered successfullt" });
        }
    }
}
