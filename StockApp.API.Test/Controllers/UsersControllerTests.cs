using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Test.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task Register_ValidUser_ReturnsOk()
        {
            var mock = new Mock<IUserService>();
            var controller = new UsersController(mock.Object);

            var dto = new UserRegisterDto
            {
                Username = "testuser",
                Password = "123456",
                Role = "User"
            };

            mock.Setup(x => x.RegisterAsync(It.IsAny<UserRegisterDto>())).ReturnsAsync(true);

            var result = await controller.Register(dto) as OkResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task Register_NullUser_ReturnsBadRequest()
        {
            var mock = new Mock<IUserService>();
            var controller = new UsersController(mock.Object);

            var result = await controller.Register(null) as BadRequestResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Register_UserExists_ReturnsConflict()
        {
            var mock = new Mock<IUserService>();
            var controller = new UsersController(mock.Object);

            var dto = new UserRegisterDto
            {
                Username = "existente",
                Password = "123456",
                Role = "User"
            };

            mock.Setup(x => x.RegisterAsync(It.IsAny<UserRegisterDto>())).ReturnsAsync(false);

            var result = await controller.Register(dto) as ConflictResult;

            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
        }
    }
}
