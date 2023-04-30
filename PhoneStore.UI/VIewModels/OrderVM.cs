using PhoneStore.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

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
