using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.UI.VIewModels;

namespace PhoneShop.UI.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private IPhonesService _phonesService;
        private IMapper _mapper;

        public PhonesController(IPhonesService phonesService, IMapper mapper)
        {
            _phonesService = phonesService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/GetAllPhones")]
        public IEnumerable<PhoneVM> GetAllPhones()
        {
            var phones = _phonesService.GetAllPhones().Phones.Select(phone => new PhoneVM()
            {
                PhoneId = phone.PhoneId,
                Brand = phone.Brand,
                Model = phone.Model,
                Color = (PhoneColorVM)phone.Color,
                Camera = phone.Camera,
                Memory = phone.Memory,
                ImageUrl = phone.ImageUrl,
                OS = phone.OS,
                RAM = phone.RAM
            });

            return phones;
        }
        [HttpGet]
        [Route("api/GetPhoneById/{id}")]
        public PhoneVM GetPhoneById(int id)
        {
            var getPhoneByIdRequest = new GetPhoneByIdRequest() { PhoneId = id};
            var phone = _mapper.Map<PhoneVM>(_phonesService.GetPhoneById(getPhoneByIdRequest).Phone);
            return phone;
        }
    }
}