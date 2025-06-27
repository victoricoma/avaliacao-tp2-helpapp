using StockApp.Domain.Entities;
namespace StockApp.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<User> GetByUsernameAsync(string username);
    }
}
