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
            var createUserResult = await _userManager.CreateAsync(user, request.Password);
            //var doesRoleExists = await _roleManager.RoleExistsAsync(request.Role);
            //if (!doesRoleExists)
            //    await _roleManager.CreateAsync(new IdentityRole() { Name = request.Role });

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

            var areCredentialsCorrect = await _userManager.CheckPasswordAsync(user, request.Password);


            if (areCredentialsCorrect)
            {
                var currentUser = await _userManager.FindByNameAsync(request.Username);
                var roles = await _userManager.GetRolesAsync(currentUser);
                var role = roles.First();

                response.CurrentUserRole = role;

                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = _configuration["JwtSettings:Secret"];
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.GivenName, request.Username),
                        //new Claim(JwtRegisteredClaimNames.Email, $"{request.Username}@gmail.com"),
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

        public async Task<GetCustomerIdByUsernameResponse> GetCustomerIdByUsername(GetCustomerIdByUsernameRequest request)
        {
            var currentUser = await _userManager.FindByNameAsync(request.Username);
            var customerId = _applicationDbContext.Customers.SingleOrDefault(c => c.UserId == currentUser.Id).CustomerId;

            var response = new GetCustomerIdByUsernameResponse() { CustomerId = customerId };
            return response;
        }
    }
}
