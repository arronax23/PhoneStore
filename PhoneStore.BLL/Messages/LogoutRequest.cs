using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PhoneStore.BLL.Messages
{
    public class LogoutRequest
    {
        public HttpContext HttpContext { get; set; }
    }
}