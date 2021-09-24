using Microsoft.AspNetCore.Identity;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Data;
using PhoneShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhoneShop.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
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
                if (request.Role == "Customer")
                {
                    _applicationDbContext.Customer.Add(new Customer() { User = user });
                    _applicationDbContext.SaveChanges();
                }
            

                response.IsSuccesfull = true;
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
            var response = new LoginResponse();
            var signInResult = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            //var currentUser = await _userManager.GetUserAsync(request.CurrentUser);
            //var us = request.CurrentUser.FindFirst(c => c.Type == ClaimTypes.Role);
            //var role =  _userManager.GetRolesAsync()

            if (signInResult.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(request.Username);
                var roles = await _userManager.GetRolesAsync(currentUser);
                var role = roles.First();

                response.CurrentUserRole = role;

                response.IsSuccesfull = true;
            }
            else
                response.IsSuccesfull = false;

            return response;
            //_signInManager.IsSignedIn()
            //await _signInManager.SignInAsync(user, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
