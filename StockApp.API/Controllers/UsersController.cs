using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo registro de usuários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema
        /// </summary>
        /// <param name="user">Dados do usuário a ser registrado</param>
        /// <returns>Resultado do registro</returns>
        /// <response code="200">Usuário registrado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="409">Conflito - usuário já existe</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto user)
        {
            if (user == null)
                return BadRequest();

            var success = await _userService.RegisterAsync(user);
            if (success)
                return Ok();
            else
                return Conflict();
        }
    }
}
