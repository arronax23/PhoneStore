using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;

namespace PhoneShop.UI.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Route("api/GetOrdersByCustomerId/{id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetOrdersByCustomerId(int id)
        {
            var resposne = _ordersService.GetOrdersByCustomerId(new GetOrdersByCustomerIdRequest() { CustomerId = id });          
            return Ok(resposne.Orders);
        }
    }
}