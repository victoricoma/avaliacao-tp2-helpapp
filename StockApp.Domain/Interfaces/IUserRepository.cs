using StockApp.Domain.Entities;
namespace StockApp.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<User> GetByUsernameAsync(string username , string password);
        Task<User> AddAsync(string username, string password);
        Task Logout();
    }
}
