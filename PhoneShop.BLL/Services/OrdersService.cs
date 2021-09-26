using Microsoft.EntityFrameworkCore;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneShop.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        private ApplicationDbContext _applicationDbContext;
        public OrdersService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public GetOrdersByCustomerIdResponse GetOrdersByCustomerId(GetOrdersByCustomerIdRequest request)
        {
            var orders = _applicationDbContext.Orders.Where(o => o.CustomerId == request.CustomerId).AsEnumerable();
            var response = new GetOrdersByCustomerIdResponse() { Orders = orders };
            return response;
        }
    }
}
