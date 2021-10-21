using AutoMapper;
using PhoneShop.DAL.Models;
using PhoneShop.UI.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.UI.AutoMapperProfiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Phone, PhoneVM>();
            CreateMap<PhoneVM, Phone>();
            //CreateMap<PhoneInOrderVM, Phone>();
            CreateMap<Phone, PhoneInOrderVM>();
        }
    }
}
