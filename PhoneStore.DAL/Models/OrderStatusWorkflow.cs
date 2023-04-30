using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public class OrderStatusWorkflow
    {
        public int OrderStatusWorkflowId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("OrderStatus")]
        public OrderStatusId OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime WorkflowDate { get; set; }
    }
}
