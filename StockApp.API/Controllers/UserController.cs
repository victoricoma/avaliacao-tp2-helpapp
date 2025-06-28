using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {       
            
            var user = new User
            {
                Username = userRegisterDTO.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password),
                Role = userRegisterDTO.Role
            };
           
             await _userRepository.AddAsync(user.Username , user.Password);
            return Ok(user);
        }
    }
}
