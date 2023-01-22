using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public class PhoneOrder
    {
        public int PhoneOrderId { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
