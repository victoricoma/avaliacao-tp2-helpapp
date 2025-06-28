using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using StockApp.Infra.Data.Identity;

namespace StockApp.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<dynamic> GetByUsernameAsync(string username)
        {
            // Implementação básica para exemplo
            // Em um cenário real, você buscaria o usuário no banco de dados
            // Aqui estamos retornando um objeto anônimo para fins de demonstração
            
            // Simula a busca de um usuário no banco de dados
            if (username == "admin")
            {
                return new
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin"
                };
            }
            else if (username == "user")
            {
                return new
                {
                    Username = "user",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
                    Role = "User"
                };
            }
            
            return null;
        }
    }
}