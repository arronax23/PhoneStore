﻿using AutoMapper;
using PhoneStore.DAL.Models;
using PhoneStore.UI.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.UI.AutoMapperProfiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Phone, PhoneVM>();
            CreateMap<Phone, OverviewPhoneVM>();
            CreateMap<PhoneVM, Phone>();
            //CreateMap<PhoneInOrderVM, Phone>();
            CreateMap<Phone, PhoneInOrderVM>();
            CreateMap<Order, OrderVM>();
        }
    }
}
