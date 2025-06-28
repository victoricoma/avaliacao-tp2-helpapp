using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo gerenciamento de usuários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema
        /// </summary>
        /// <param name="registerDto">Dados de registro do usuário</param>
        /// <returns>Resultado do registro</returns>
        /// <response code="200">Usuário registrado com sucesso</response>
        /// <response code="400">Dados inválidos ou usuário não pode ser registrado</response>
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

