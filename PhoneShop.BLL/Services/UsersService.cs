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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
        {
            var user = new ApplicationUser() { UserName = request.Username };
            var createResult = await _userManager.CreateAsync(user, request.Password);
            var doesRoleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (!doesRoleExists)
                await _roleManager.CreateAsync(new IdentityRole() { Name = request.Role });

            IdentityResult roleResult = new IdentityResult();
            if (createResult.Succeeded)
                roleResult = await _userManager.AddToRoleAsync(user, request.Role);
            var response = new RegisterUserResponse();
            
            if (createResult.Succeeded && roleResult.Succeeded)
            {
                response.IsSuccesfull = true;
                //throw new Exception("Registering failed");
            }
            else
            {
                response.Errors = new List<IdentityError>();
                response.IsSuccesfull = false;
                response.Errors.AddRange(createResult.Errors);
                if (roleResult.Errors != null)
                    response.Errors.AddRange(roleResult.Errors);
            }
            return response;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            var response = new LoginResponse();
            if (signInResult.Succeeded)
                response.IsSuccesfull = true;
            else
                response.IsSuccesfull = false;

            return response;
            //_signInManager.IsSignedIn()
            //await _signInManager.SignInAsync(user, false);
        }
    }
}
