using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.BLL.Messages
{
    public class RegisterUserResponse
    {
        public bool IsSuccesfull { get; set; }
        public IEnumerable<IdentityError> Errors  { get; set; }
    }
}
