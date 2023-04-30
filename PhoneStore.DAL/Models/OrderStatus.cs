using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public enum OrderStatusId { Open = 0, Closed = 1, Paid = 2, Delivered = 3 };

    public class OrderStatus
    {
        public OrderStatusId OrderStatusId { get; set; }
        public string Status { get; set; }

        public List<OrderStatusWorkflow> OrderStatusWorkflow { get; set; }
        public List<Order> Order { get; set; }
    }
}
