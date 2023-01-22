using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public enum OrderStatus { Open = 0, Closed = 1, Paid = 2, Delivered = 3 };
    public class Order
    {
        public int OrderId { get; set; }
        public List<PhoneOrder> PhoneOrder { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderStatusWorkflow> OrderStatusWorkflow { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
