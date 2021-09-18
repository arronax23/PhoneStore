using System;
using System.Collections.Generic;
using System.Linq;
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
            await _usersService.RegisterUser(new RegisterUserRequest() { Username = userVM.Username, Password = userVM.Password });
            return Ok();
        }
    }
}