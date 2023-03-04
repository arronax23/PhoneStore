using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPasswordHasher<ApplicationUser> _hasher;

        public UsersService(ApplicationDbContext applicationDbContext, IPasswordHasher<ApplicationUser> hasher)
        {
            _applicationDbContext = applicationDbContext;
            _hasher = hasher;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request)
        {
            var response = new RegisterUserResponse();

            var user = new ApplicationUser()
            {
                Username = request.Username,
                Claims = new List<UserClaim>
                {   
                    new UserClaim()
                    {
                        Type = ClaimTypes.Name,
                        Value = request.Username
                    },
                    new UserClaim()
                    {
                        Type = ClaimTypes.Role,
                        Value = request.Role
                    }
                }
            };
            user.PasswordHash = _hasher.HashPassword(user, request.Password);

            await _applicationDbContext.ApplicationUsers.AddAsync(user);

            if (request.Role == "Customer")
            {
                await _applicationDbContext.Customers.AddAsync(new Customer() { ApplicationUser = user });
                await _applicationDbContext.SaveChangesAsync();
                response.IsSuccesfull = true;
                response.Errors = new List<IdentityError>();
            }
            else if (request.Role == "Admin")
            {
                await _applicationDbContext.SaveChangesAsync();
                response.IsSuccesfull = true;
                response.Errors = new List<IdentityError>();
            }
            else
            {
                response.IsSuccesfull = false;
                response.Errors = new List<IdentityError>();
            }

            return response;

        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var response = new LoginResponse();

            var user = await _applicationDbContext
                .ApplicationUsers
                .Include(au => au.Claims)
                .SingleOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                response.IsSuccesfull = false;
                return response;
            }

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Success) 
            {
                var claims = user.Claims.Select(c => new Claim(c.Type, c.Value));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                response.IsSuccesfull = true;
                response.CurrentUserRole = claims.SingleOrDefault(c => c.Type == ClaimTypes.Role).Value;
            }
            else
            {
                response.IsSuccesfull = false;
            }

            return response;
        }

        public async Task Logout(LogoutRequest request)
        {
            await request.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<GetCustomerIdByUsernameResponse> GetCustomerIdByUsername(GetCustomerIdByUsernameRequest request)
        {

            var user = await _applicationDbContext
                .ApplicationUsers
                .Include(a => a.Customer)
                .SingleOrDefaultAsync(u => u.Username == request.Username);

            var customerId = user.Customer.CustomerId;

            var response = new GetCustomerIdByUsernameResponse() { CustomerId = customerId };
            return response;
        }
    }
}
