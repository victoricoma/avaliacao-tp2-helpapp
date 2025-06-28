using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using StockApp.Infra.Data.Identity;
using StockApp.Domain.Interfaces;
using Xunit;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["Jwt:Key"]).Returns("supersecretkey1234567890");
        _configurationMock.Setup(c => c["Jwt:ExpireMinutes"]).Returns("60");
        _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task AuthenticateAsync_ReturnsToken_WhenCredentialsAreValid()
    {
        // Arrange
        var username = "testuser";
        var password = "password123";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new ApplicationUser { UserName = username, PasswordHash = passwordHash, Role = "User" };
        _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync(user);

        // Act
        var result = await _authService.AuthenticateAsync(username, password);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result.Token));
        Assert.True(result.Expiration > DateTime.UtcNow);
    }

    [Fact]
    public async Task AuthenticateAsync_ReturnsNull_WhenUserNotFound()
    {
        // Arrange
        var username = "nonexistent";
        var password = "password123";
        _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await _authService.AuthenticateAsync(username, password);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_ReturnsNull_WhenPasswordIsInvalid()
    {
        // Arrange
        var username = "testuser";
        var password = "wrongpassword";
        var user = new ApplicationUser { UserName = username, PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), Role = "User" };
        _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(username)).ReturnsAsync(user);

        // Act
        var result = await _authService.AuthenticateAsync(username, password);

        // Assert
        Assert.Null(result);
    }
}
