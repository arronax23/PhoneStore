using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.UI.VIewModels
{
    public class PhoneInOrderVM
    {
        public int PhoneId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public PhoneColorVM Color { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}
