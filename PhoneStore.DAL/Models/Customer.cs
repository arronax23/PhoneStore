using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
