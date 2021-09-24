using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.DAL.Models
{
    public enum PhoneColor { White, Black, Red, Blue, Pink }
    public class Phone
    { 
        public int PhoneId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Memory { get; set; }
        public int Camera { get; set; }
        public PhoneColor Color  { get; set; }
        public string ImageUrl  { get; set; }
        public float Price  { get; set; }
        public List<PhoneOrder> PhoneOrder { get; set; }
    }
}
