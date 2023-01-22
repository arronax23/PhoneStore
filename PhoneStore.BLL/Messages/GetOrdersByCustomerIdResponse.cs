using PhoneStore.DAL.Models;
using System.Collections.Generic;

namespace PhoneStore.BLL.Messages
{
    public class GetOrdersByCustomerIdResponse
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}