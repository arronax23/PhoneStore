using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<PhoneOrder> PhoneOrder { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderStatusWorkflow> OrderStatusWorkflow { get; set; }
        [ForeignKey("OrderStatus")]
        public OrderStatusId OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
