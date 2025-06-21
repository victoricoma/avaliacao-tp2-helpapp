using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using System;
using StockApp.Application.DTOs;
using StockApp.API;
using Microsoft.Win32;

namespace StockApp.Domain.Test
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public IntegrationTests(WebApplicationFactory<Program> factory) 
        { 
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterAndLogin_ValidCredentials_ReturnsToken()
        {
            var userRegisterDto = new UserRegisterDto
            {
                UserName = "testuser321",
                Password = "password123",
                Role = "User"
            };

            var userLoginDto = new UserLoginDto
            {
                UserName = "testuser321",
                Password = "password123"
            };

            var registerResponse = await _client.PostAsJsonAsync("/api/users/register", userRegisterDto);
            registerResponse.EnsureSuccessStatusCode();

            var loginResponse = await _client.PostAsJsonAsync("/api/token/login", userLoginDto);
            loginResponse.EnsureSuccessStatusCode();

            var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDto>();

            Assert.NotNull(tokenResponse);
            Assert.False((string.IsNullOrEmpty(tokenResponse.Token)));
            Assert.True(tokenResponse.Expiration > DateTime.UtcNow);
        }

        [Fact]
        public async Task Register_WithExistingUsername_ReturnsConflict()
        {
            var uniqueUser = $"user_{Guid.NewGuid()}";

            var userRegisterDto = new UserRegisterDto
            {
                UserName = uniqueUser,
                Password = "password123",
                Role = "User"
            };

            var firstResponse = await _client.PostAsJsonAsync("/api/users/register", userRegisterDto);
            firstResponse.EnsureSuccessStatusCode();

            var secondResponse = await _client.PostAsJsonAsync("/api/users/register", userRegisterDto);
            Assert.Equal(System.Net.HttpStatusCode.Conflict, secondResponse.StatusCode);
        }

        [Fact]
        public async Task Register_WithoutPassword_ReturnsBadRequest()
        {
            var userRegisterDto = new UserRegisterDto
            {
                UserName = $"user_{Guid.NewGuid()}",
                Password = "",
                Role = "User"
            };

            var response = await _client.PostAsJsonAsync("/api/users/register", userRegisterDto);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_WithInvalidPassword_ReturnUnauthorized()
        {
            var uniqueUser = $"testuser_{Guid.NewGuid()}";

            var userRegisterDto = new UserRegisterDto
            {
                UserName = uniqueUser,
                Password = "correctPassword",
                Role = "User"
            };

            var registerResponse = await _client.PostAsJsonAsync("/api/users/register", userRegisterDto);
            registerResponse.EnsureSuccessStatusCode();

            var userLoginDto = new UserLoginDto
            {
                UserName = uniqueUser,
                Password = "wrongPassword"
            };

            var loginResponse = await _client.PostAsJsonAsync("/api/token/login", userLoginDto);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, loginResponse.StatusCode);
        }

        [Fact]
        public async Task Login_NonExistentUser_ReturnsUnauthorizes()
        {
            var userLoginDto = new UserLoginDto
            {
                UserName = $"noneexistent_{Guid.NewGuid()}",
                Password = "anyPassword"
            };

            var loginResponse = await _client.PostAsJsonAsync("/api/token/login", userLoginDto);

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, loginResponse.StatusCode);
        }
    }
}