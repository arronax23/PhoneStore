using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Models;
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
            //var phones = _phonesService.GetAllPhones().Phones.Select(phone => new PhoneVM()
            //{
            //    PhoneId = phone.PhoneId,
            //    Brand = phone.Brand,
            //    Model = phone.Model,
            //    Color = (PhoneColorVM)phone.Color,
            //    Camera = phone.Camera,
            //    Memory = phone.Memory,
            //    ImageUrl = phone.ImageUrl,
            //    OS = phone.OS,
            //    RAM = phone.RAM
            //});

            var phones = _phonesService.GetAllPhones().Phones;
            var phonesVM = _mapper.Map<IEnumerable<PhoneVM>>(phones);

            return phonesVM;
        }
        [HttpGet]
        [Route("api/GetPhoneById/{id}")]
        public PhoneVM GetPhoneById(int id)
        {
            var getPhoneByIdRequest = new GetPhoneByIdRequest() { PhoneId = id};
            var phoneVM = _mapper.Map<PhoneVM>(_phonesService.GetPhoneById(getPhoneByIdRequest).Phone);
            return phoneVM;
        }

        [HttpPost]
        [Route("api/SavePhone")]
        public IActionResult SavePhone([FromBody] PhoneVM phoneVM)
        {
            var phone = _mapper.Map<Phone>(phoneVM);
            _phonesService.SavePhone(new SavePhoneRequest() { Phone = phone });
            return Ok(phone);
        }
        [HttpDelete]
        [Route("api/DeletePhoneById/{id}")]
        public IActionResult DeletePhoneById(int id)
        {
            _phonesService.DeletePhoneById(new DeletePhoneByIdRequest() { PhoneId = id });
            return Ok();
        }

    }
}