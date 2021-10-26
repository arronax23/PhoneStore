using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IPhonesService _phonesService;
        private readonly IMapper _mapper;

        public PhonesController(IPhonesService phonesService, IMapper mapper)
        {
            _phonesService = phonesService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/GetAllPhones")]
        [Authorize(Roles = "Admin,Customer")]
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
        [Route("api/GetPhonesForOnePage")]
        [Authorize(Roles = "Admin,Customer")]
        public IEnumerable<PhoneVM> GetPhonesForOnePage(int pageNumber)
        {
            var phones = _phonesService.GetPhonesForOnePage(new GetPhonesForOnePageRequest() { PageNumber = pageNumber }).Phones;
            var phonesVM = _mapper.Map<IEnumerable<PhoneVM>>(phones);

            return phonesVM;
        }

        [HttpGet]
        [Route("api/SearchPhones")]
        [Authorize(Roles = "Admin,Customer")]
        public IEnumerable<PhoneVM> SearchPhones(string searchText)
        {
            var phones = _phonesService.SearchPhones(new SearchPhonesRequest() { SearchText = searchText }).Phones;
            var phonesVM = _mapper.Map<IEnumerable<PhoneVM>>(phones);

            return phonesVM;
        }

        [HttpGet]
        [Route("api/GetPhoneById/{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public PhoneVM GetPhoneById(int id)
        {
            var getPhoneByIdRequest = new GetPhoneByIdRequest() { PhoneId = id };
            var phoneVM = _mapper.Map<PhoneVM>(_phonesService.GetPhoneById(getPhoneByIdRequest).Phone);
            return phoneVM;
        }

        [HttpGet]
        [Route("api/GetNumberOfPagesInPhoneList")]
        [Authorize(Roles = "Admin,Customer")]
        public int GetNumberOfPagesInPhoneList()
        {
            var numberOfPagesInPhoneList = _phonesService.GetNumberOfPagesInPhoneList().NumberOfPages;
            return numberOfPagesInPhoneList;
        }

        [HttpPost]
        [Route("api/CreatePhone")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePhone(PhoneVM phoneVM)
        {
            var phone = _mapper.Map<Phone>(phoneVM);
            _phonesService.CreatePhone(new SavePhoneRequest() { Phone = phone });
            return Created($"api/GetPhoneById/{phone.PhoneId}", phone);
        }

        [HttpDelete]
        [Route("api/DeletePhoneById/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletePhoneById(int id)
        {
            _phonesService.DeletePhoneById(new DeletePhoneByIdRequest() { PhoneId = id });
            return Ok();
        }

        [HttpPut]
        [Route("api/UpdatePhone")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePhone(PhoneVM phoneVM)
        {
            var phone = _mapper.Map<Phone>(phoneVM);
            _phonesService.UpdatePhone(new UpdatePhoneRequest() { Phone = phone });
            return Ok();
        }

        [HttpPost]
        [Route("api/AddPhoneToShoppingCart")]
        [Authorize(Roles = "Customer")]
        public IActionResult AddPhoneToShoppingCart(AddPhoneToShoppingCardVM addPhoneToShoppingCardVM)
        {
            _phonesService.AddPhoneToShoppingCart(new AddPhoneToShoppingCardRequest()
            {
                CustomerId = addPhoneToShoppingCardVM.CustomerId,
                PhoneId = addPhoneToShoppingCardVM.PhoneId
            });
            return Ok();
        }

        [HttpGet]
        [Route("api/IsPhoneInShoppingCart")]
        [Authorize(Roles = "Customer")]
        public IActionResult IsPhoneInShoppingCart(int customerId, int phoneId)
        {
            var response = _phonesService.IsPhoneInShoppingCart(new IsPhoneInShoppingCartRequest() { CustomerId = customerId, PhoneId = phoneId });
            return Ok(response.IsPhoneInShoppingCart);
        }

        [HttpPost]
        [Route("api/RemovePhoneFromShoppingCart")]
        [Authorize(Roles = "Customer")]
        public IActionResult RemovePhoneFromShoppingCart(RemovePhoneFromShoppingCartVM removePhoneFromShoppingCartVM)
        {
            _phonesService.RemovePhoneFromShoppingCart(new RemovePhoneFromShoppingCartRequest()
            {
                CustomerId = removePhoneFromShoppingCartVM.CustomerId,
                PhoneId = removePhoneFromShoppingCartVM.PhoneId
            });
            return Ok();
        }

        [HttpGet]
        [Route("api/GetPhonesInOrder")]
        [Authorize(Roles = "Customer")]
        public IEnumerable<PhoneInOrderVM> GetPhonesInOrder(int orderId)
        {
            var response = _phonesService.GetPhonesInOrder(new GetPhonesInOrderRequest() { OrderId = orderId });
            var phoneInOrderVM = _mapper.Map<IEnumerable<PhoneInOrderVM>>(response.Phones);

            return phoneInOrderVM;
        }

    }


}