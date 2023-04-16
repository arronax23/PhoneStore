using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.BLL.Interfaces;
using PhoneStore.BLL.Messages;

namespace PhoneStore.UI.Controllers
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

        [HttpPost]
        [Route("api/ChangeOrderStatus")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, string newStatus)
        {
            var isChanged = await _ordersService.ChangeOrderStatus(new ChangeOrderStatusRequest() 
            { 
                OrderId = orderId,
                NewStatus = newStatus 
            });

            if (isChanged == false)
                return BadRequest(new {Error = "Status didn't change" });

            return NoContent();
        }
    }
}