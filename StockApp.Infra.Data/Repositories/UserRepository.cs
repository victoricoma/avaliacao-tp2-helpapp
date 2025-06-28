using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StockApp.Domain.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetByUsernameAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                return user;
            }
            return null;

        }

        public async Task<User> AddAsync(string username, string password)
        {
            var user = new User
            {
                Username = username
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return user;
            }
            return null;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

