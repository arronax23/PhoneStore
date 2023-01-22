using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public List<Order> Orders { get; set; }
    }
}
