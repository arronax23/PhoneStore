using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public class OrderStatusWorkflow
    {
        public int OrderStatusWorkflowId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime WorkflowDate { get; set; }
    }
}
