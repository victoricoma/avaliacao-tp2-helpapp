using System.Threading.Tasks;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        

        public async Task<bool> RegisterAsync(UserRegisterDto userDto)
        {
            // 1. Validações básicas
            if (string.IsNullOrWhiteSpace(userDto.Username) ||
                string.IsNullOrWhiteSpace(userDto.Password) ||
                string.IsNullOrWhiteSpace(userDto.Role))
            {
                return false;
            }
                
            if (userDto.Username.Length < 3 || userDto.Username.Length > 20)
                return false;
            
            if (userDto.Password.Length < 8)
                return false;
                
                return true;
        }
    }
}