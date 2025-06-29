using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using StockApp.Application.DTOs;
using StockApp.Infra.Data.Identity;


namespace StockApp.Domain.Test
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var configurationMock = new Mock<IConfiguration>();

            var user = new User
            {
                Username = "testuser",
                Password = BCrypt.Net.BCrypt.HashPassword("password"),
                Role = "User"
            };

            userRepositoryMock
                .Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            configurationMock.Setup(config => config["JwtSettings:SecretKey"]).Returns("ChaveSuperSecretaParaTestesJwtToken1234567890");
            configurationMock.Setup(config => config["JwtSettings:AccessTokenExpiration"]).Returns("60");

            var authService = new AuthService(userRepositoryMock.Object, configurationMock.Object);

            // Act
            var result = await authService.AuthenticateAsync("testuser", "password");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TokenResponseDTO>(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
            Assert.True(result.Expiration > DateTime.UtcNow);
        }
    }
}