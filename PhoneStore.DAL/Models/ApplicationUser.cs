using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace PhoneStore.DAL.Models
{
    public class ApplicationUser
    {
        public int ApplicationUserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Customer Customer { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
