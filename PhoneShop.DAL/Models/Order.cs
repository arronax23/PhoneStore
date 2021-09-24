using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.DAL.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<PhoneOrder> PhoneOrder { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
