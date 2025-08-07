using System;
using PhoneStore.DAL.Models;

namespace PhoneStore.UI.VIewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public OrderStatusId OrderStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
