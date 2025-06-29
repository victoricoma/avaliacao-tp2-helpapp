using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using Xunit;

namespace StockApp.Domain.Test.TestControllers
{
    public class TokenControllerTests
    {
        [Fact]
        public void Login_ValidCredentials_ReturnsToken()
        {
            var controller = new TokenController();

            var loginDto = new UserLoginDto
            {
                UserName = "testuser321",
                Password = "password123"
            };

            var result =  controller.Login(loginDto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            
            var token = result.Value as TokenResponseDto;
            Assert.NotNull(token);
            Assert.Equal("fake-jwt-token", token.Token);
        }

        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            var controller = new TokenController();

            var loginDto = new UserLoginDto
            {
                UserName = "usuarioErrado",
                Password = "senhaErrada"
            };

            var result = controller.Login(loginDto);

            Assert.IsType<UnauthorizedObjectResult>(result);

            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.NotNull(unauthorizedResult);
            Assert.Equal("Invalid credentials", unauthorizedResult.Value);
        }
    }
}
