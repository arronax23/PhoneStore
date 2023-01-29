﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhoneStore.BLL.Interfaces;
using PhoneStore.BLL.Messages;
using PhoneStore.DAL.Data;
using PhoneStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.BLL.Services
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
            var createUserResult = await _userManager.CreateAsync(user, request.Password);

            IdentityResult addToRoleResult = new IdentityResult();
            if (createUserResult.Succeeded)
                addToRoleResult = await _userManager.AddToRoleAsync(user, request.Role);

            
            var response = new RegisterUserResponse();
            
            if (createUserResult.Succeeded && addToRoleResult.Succeeded)
            {
                if (request.Role == "Customer")
                {
                    _applicationDbContext.Customers.Add(new Customer() { User = user });
                    _applicationDbContext.SaveChanges();
                }
            
                response.IsSuccesfull = true;
            }
            else
            {
                response.Errors = new List<IdentityError>();
                response.IsSuccesfull = false;
                response.Errors.AddRange(createUserResult.Errors);
                if (addToRoleResult.Errors != null)
                    response.Errors.AddRange(addToRoleResult.Errors);
            }
            return response;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = new LoginResponse();

            var user = await _userManager.FindByNameAsync(request.Username);
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);

            if (isPasswordCorrect)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.First();
                response.CurrentUserRole = role;
                response.IsSuccesfull = true;
                return response;
            }
            else 
                response.IsSuccesfull = false;   

            return response;
        }

        public async Task Logout( )
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<GetCustomerIdByUsernameResponse> GetCustomerIdByUsername(GetCustomerIdByUsernameRequest request)
        {
            var currentUser = await _userManager.FindByNameAsync(request.Username);
            var customerId = _applicationDbContext.Customers.SingleOrDefault(c => c.UserId == currentUser.Id).CustomerId;

            var response = new GetCustomerIdByUsernameResponse() { CustomerId = customerId };
            return response;
        }
    }
}
