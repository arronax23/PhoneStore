using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.DAL.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public Order Order { get; set; }
    }
}
