using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var success = await _userService.RegisterAsync(registerDto);
            if (!success)
                return BadRequest("Dados Inválidos ou usuário não pode ser registrado");
            return Ok("Usuário registrado com sucesso");
        }
    }
}

