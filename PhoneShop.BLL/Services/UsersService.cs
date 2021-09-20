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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
        {
            var user = new ApplicationUser() { UserName = request.Username };
            var createResult = await _userManager.CreateAsync(user, request.Password);
            var doesRoleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (!doesRoleExists)
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin"});
            var roleresult = await _userManager.AddToRoleAsync(user, request.Role);
            var response = new RegisterUserResponse();
            

            if (createResult.Succeeded && roleresult.Succeeded)
            {
                response.IsSuccesfull = true;
                //throw new Exception("Registering failed");
            }
            else
            {
                response.IsSuccesfull = false;
                response.Errors.AddRange(createResult.Errors);
                response.Errors.AddRange(roleresult.Errors);
            }
            return response;
        }
    }
}
