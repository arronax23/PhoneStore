using AutoMapper;
using PhoneStore.DAL.Models;
using PhoneStore.UI.VIewModels;

namespace PhoneStore.UI.AutoMapperProfiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Phone, PhoneVM>();
            CreateMap<Phone, OverviewPhoneVM>();
            CreateMap<PhoneVM, Phone>();
            CreateMap<Phone, PhoneInOrderVM>();
            CreateMap<Order, OrderVM>();
        }
    }
}
