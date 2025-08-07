using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.BLL.Interfaces;
using PhoneStore.BLL.Messages;
using PhoneStore.DAL.Models;
using PhoneStore.UI.VIewModels;

namespace PhoneStore.UI.Controllers
{
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
        public IActionResult GetAllPhones()
        {
            var phones = _phonesService.GetAllPhones().Phones;

            if (phones == null)
                return NotFound();

            var phonesVM = _mapper.Map<IEnumerable<PhoneVM>>(phones);

            return Ok(phonesVM);
        }


        [HttpGet]
        [Route("api/GetPhonesForOnePage")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult GetPhonesForOnePage(int pageNumber)
        {
            var phones = _phonesService.GetPhonesForOnePage(new GetPhonesForOnePageRequest() { PageNumber = pageNumber }).Phones;

            if (phones == null)
                return NotFound();

            var phonesVM = _mapper.Map<IEnumerable<OverviewPhoneVM>>(phones);

            return Ok(phonesVM);
        }

        [HttpGet]
        [Route("api/SearchPhones")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult SearchPhones(string searchText)
        {
            var phones = _phonesService.SearchPhones(new SearchPhonesRequest() { SearchText = searchText }).Phones;

            if (phones == null)
                return NotFound();

            var phonesVM = _mapper.Map<IEnumerable<PhoneVM>>(phones);
            return Ok(phonesVM);
        }

        [HttpGet]
        [Route("api/GetPhoneById/{id}")]
        [Authorize(Roles = "Admin,Customer")]
        public IActionResult GetPhoneById(int id)
        {
            var getPhoneByIdRequest = new GetPhoneByIdRequest() { PhoneId = id };
            var phone = _phonesService.GetPhoneById(getPhoneByIdRequest).Phone;
            
            if (phone == null)
                return NotFound();

            var phoneVM = _mapper.Map<PhoneVM>(phone);
            return Ok(phoneVM);
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
        public async Task<IActionResult> CreatePhone(PhoneVM phoneVM)
        {
            var phone = _mapper.Map<Phone>(phoneVM);
            var isCreated = await _phonesService.CreatePhone(new SavePhoneRequest() { Phone = phone });

            if (isCreated == false)
                return BadRequest(new { Error = "No phone was created" });

            return Created($"api/GetPhoneById/{phone.PhoneId}", phone);
        }

        [HttpDelete]
        [Route("api/DeletePhoneById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePhoneById(int id)
        {
            var isDeleted = await _phonesService.DeletePhoneById(new DeletePhoneByIdRequest() { PhoneId = id });

            if (isDeleted == false)
                return BadRequest(new { Error = "No phone was deleted" });

            return NoContent();
        }

        [HttpPut]
        [Route("api/UpdatePhone")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePhone(PhoneVM phoneVM)
        {
            var phone = _mapper.Map<Phone>(phoneVM);
            var isUpdated = await _phonesService.UpdatePhone(new UpdatePhoneRequest() { Phone = phone });

            if (isUpdated == false)
                return BadRequest(new { Error = "No phone was updated" });

            return NoContent();
        }

        [HttpPost]
        [Route("api/AddPhoneToShoppingCart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddPhoneToShoppingCart(AddPhoneToShoppingCardVM addPhoneToShoppingCardVM)
        {
            var isAdded = await _phonesService.AddPhoneToShoppingCart(new AddPhoneToShoppingCardRequest()
            {
                CustomerId = addPhoneToShoppingCardVM.CustomerId,
                PhoneId = addPhoneToShoppingCardVM.PhoneId
            });

            if (isAdded == false)
                return BadRequest(new { Error = "No phone was added to shopping cart" });

            return NoContent();
        }

        [HttpGet]
        [Route("api/IsPhoneInShoppingCart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> IsPhoneInShoppingCart(int customerId, int phoneId)
        {
            var response = await _phonesService.IsPhoneInShoppingCart(new IsPhoneInShoppingCartRequest() { CustomerId = customerId, PhoneId = phoneId });
            return Ok(response.IsPhoneInShoppingCart);
        }

        [HttpPost]
        [Route("api/RemovePhoneFromShoppingCart")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> RemovePhoneFromShoppingCart(RemovePhoneFromShoppingCartVM removePhoneFromShoppingCartVM)
        {
            var isDeleted = await _phonesService.RemovePhoneFromShoppingCart(new RemovePhoneFromShoppingCartRequest()
            {
                CustomerId = removePhoneFromShoppingCartVM.CustomerId,
                PhoneId = removePhoneFromShoppingCartVM.PhoneId
            });

            if (isDeleted == false)
                return BadRequest(new { Error = "No phone was removed from shopping cart" });

            return NoContent();
        }

        [HttpGet]
        [Route("api/GetPhonesInOrder")]
        [Authorize(Roles = "Customer")]
        public IEnumerable<PhoneInOrderVM> GetPhonesInOrder(int orderId)
        {
            var response = _phonesService.GetPhonesInOrder(new GetPhonesInOrderRequest() { OrderId = orderId });
            var phonesInOrderVM = _mapper.Map<IEnumerable<PhoneInOrderVM>>(response.Phones);

            return phonesInOrderVM;
        }


        [HttpDelete]
        [Route("api/DeleteCustomerById/{id}")]
        public IActionResult DeleteCustomerById(int id)
        {
            var isDeleted =  _phonesService.RemoveCustomer(new RemoveCustomerRequest() { CustomerId = id });

            if (isDeleted == false)
                return BadRequest(new { Error = "No phone was deleted" });

            return NoContent();
        }
    }
}