using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhoneStore.BLL.Interfaces;
using PhoneStore.BLL.Messages;
using PhoneStore.BLL.Services;
using PhoneStore.DAL.Models;
using PhoneStore.UI.VIewModels;

namespace PhoneStore.UI.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService usersService, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/Register")]
        public async Task<IActionResult> Register(UserVM userVM)
        {
            var response  = await _usersService.RegisterUser(new RegisterUserRequest()
            {
                Username = userVM.Username,
                Password = userVM.Password,
                Role = userVM.Role
            });

            if (response.IsSuccesfull == true)
                return Ok();
            else
            {
                StringBuilder errorVM = new StringBuilder();
                foreach (var error in response.Errors)
                {
                    errorVM.Append(error.Description+" ");
                }
                return BadRequest(errorVM.ToString());
            }
        }
        [HttpPost]
        [Route("api/Login")]
        public async Task<IActionResult> Login(UserVM userVM)
        {
            var response = await _usersService.Login(new LoginRequest()
            {
                Username = userVM.Username,
                Password = userVM.Password
            });

            if (response.IsSuccesfull)

            {
                _logger.LogInformation($"Succesufully logged in user: {userVM.Username}");
                return Ok(response);
            }
            else
            {
                _logger.LogInformation($"Failed to log in user: {userVM.Username}");
                return BadRequest("Logging failed");
            }

        }


        [HttpGet]
        [Route("api/GetCustomerIdByUsername/{username}")]
        public async Task<IActionResult> GetCustomerIdByUsername(string username)
        {
            var response = await _usersService.GetCustomerIdByUsername(new GetCustomerIdByUsernameRequest() { Username = username });
            return Ok(response.CustomerId);
        }
    }
}