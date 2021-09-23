using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.BLL.Services;
using PhoneShop.DAL.Models;
using PhoneShop.UI.VIewModels;

namespace PhoneShop.UI.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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
                return Problem(errorVM.ToString());
            }
        }
        [HttpPost]
        [Route("api/Login")]
        public async Task<IActionResult> Login(UserVM userVM)
        {
            var response = await _usersService.Login(new LoginRequest()
            {
                Username = userVM.Username,
                Password = userVM.Password,
                CurrentUser = HttpContext.User
            });

            if (response.IsSuccesfull)
                return Ok(response.CurrentUserRole);
            else
                return Problem("Logging failed");
        }
    }
}