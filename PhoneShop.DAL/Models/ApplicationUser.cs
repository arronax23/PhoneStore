using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Customer Customer { get; set; }
    }
}
