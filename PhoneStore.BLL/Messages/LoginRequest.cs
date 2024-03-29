﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PhoneStore.BLL.Messages
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}