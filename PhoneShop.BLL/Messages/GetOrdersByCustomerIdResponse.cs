using PhoneShop.DAL.Models;
using System.Collections.Generic;

namespace PhoneShop.BLL.Messages
{
    public class GetOrdersByCustomerIdResponse
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}