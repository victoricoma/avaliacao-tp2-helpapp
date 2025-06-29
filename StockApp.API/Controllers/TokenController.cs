using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMfaService _mfaService;

        public TokenController(IAuthService authService, IMfaService mfaService)
        {
            _authService = authService;
            _mfaService = mfaService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
        {
            var token = await _authService.AuthenticateAsync(userLoginDto.Username, userLoginDto.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet("generate-otp")]
        public IActionResult GenerateOtp()
        {
            var otp = _mfaService.GenerateOtp();

            return Ok(new { otp });
        }

        [HttpPost("validate-otp")]
        public IActionResult ValidateOtp([FromBody] OtpRequest request)
        {
            var isValid = _mfaService.ValidateOtp(request.UserOtp, request.StoredOtp);
            return isValid ? Ok("OTP v�lido") : BadRequest("OTP inv�lido");
        }

        public class OtpRequest
        {
            public string UserOtp { get; set; }
            public string StoredOtp { get; set; }
        }
    }
}