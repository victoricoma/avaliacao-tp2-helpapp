using System.Threading.Tasks;
using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserRegisterDto user);
    }
}
