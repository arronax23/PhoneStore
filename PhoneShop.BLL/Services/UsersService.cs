using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Data;
using PhoneShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly IConfiguration _configuration;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext applicationDbContext,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
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
                    _applicationDbContext.Customers.Add(new Customer() { User = user });
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

            var user = await _userManager.FindByNameAsync(request.Username);

            var AreCredentialsCorrect = await _userManager.CheckPasswordAsync(user, request.Password);


            if (AreCredentialsCorrect)
            {
                var currentUser = await _userManager.FindByNameAsync(request.Username);
                var roles = await _userManager.GetRolesAsync(currentUser);
                var role = roles.First();

                response.CurrentUserRole = role;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                response.Token = tokenHandler.WriteToken(token);

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

        public async Task<GetCustomerIdByUsernameResponse> GetCustomerIdByUsername(GetCustomerIdByUsernameRequest request)
        {
            var currentUser = await _userManager.FindByNameAsync(request.Username);
            var customerId = _applicationDbContext.Customers.SingleOrDefault(c => c.UserId == currentUser.Id).CustomerId;

            var response = new GetCustomerIdByUsernameResponse() { CustomerId = customerId };
            return response;
        }
    }
}
