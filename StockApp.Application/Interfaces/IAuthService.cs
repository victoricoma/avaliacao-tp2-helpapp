using System.Threading.Tasks;
using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> AuthenticateAsync(string username, string password);
    }
}

