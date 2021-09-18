using Microsoft.AspNetCore.Identity;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneShop.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task RegisterUser(RegisterUserRequest request)
        {
            var user = new ApplicationUser() { UserName = request.Username };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new Exception("Registering failed");
        }
    }
}
