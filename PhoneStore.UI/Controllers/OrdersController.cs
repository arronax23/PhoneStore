using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.BLL.Interfaces;
using PhoneStore.BLL.Messages;
using PhoneStore.UI.VIewModels;

namespace PhoneStore.UI.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/GetOrdersByCustomerId/{id}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetOrdersByCustomerId(int id)
        {
            var resposne = _ordersService.GetOrdersByCustomerId(new GetOrdersByCustomerIdRequest() { CustomerId = id });
            var orders = resposne.Orders;

            if (orders == null)
                return NotFound();

            var ordersVM = _mapper.Map<IEnumerable<OrderVM>>(orders);

            return Ok(ordersVM);
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