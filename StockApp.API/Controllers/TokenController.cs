using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação de usuários
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;

        public TokenController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza o login do usuário e retorna um token JWT
        /// </summary>
        /// <param name="userLogin">Dados de login do usuário</param>
        /// <returns>Token JWT e data de expiração</returns>
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login([FromBody] UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _authService.AuthenticateAsync(userLogin.Username, userLogin.Password);
            if (token == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            return Ok(token);
        }
    }
}