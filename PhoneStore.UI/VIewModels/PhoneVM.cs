﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.UI.VIewModels
{
    public enum PhoneColorVM { White, Black, Red, Blue, Pink }
    public class PhoneVM
    {
        public int PhoneId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string OS { get; set; }
        public int RAM { get; set; }
        public int Memory { get; set; }
        public int Camera { get; set; }
        public PhoneColorVM Color { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}
