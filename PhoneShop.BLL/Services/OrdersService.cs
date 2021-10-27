using Microsoft.EntityFrameworkCore;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Data;
using PhoneShop.DAL.Models;
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
        public bool ChangeOrderStatus(ChangeOrderStatusRequest request)
        {
            var order = _applicationDbContext.Orders.Include(o => o.OrderStatusWorkflow).SingleOrDefault(o => o.OrderId == request.OrderId);
            if (order == null)
                throw new Exception("No order was found.");

            var newStatus = OrderStatus.Open;

            switch (request.NewStatus)
            {
                case "Open":
                    newStatus = OrderStatus.Open;
                    break;
                case "Closed":
                    newStatus = OrderStatus.Closed;
                    break;
                case "Paid":
                    newStatus = OrderStatus.Paid;
                    break;
                case "Delivered":
                    newStatus = OrderStatus.Delivered;
                    break;
                default:
                    newStatus = OrderStatus.Open;
                    break;
            }

            order.Status = newStatus;
            order.ModifiedDate = DateTime.Now;
            order.OrderStatusWorkflow.Add(new OrderStatusWorkflow()
            {
                Status = newStatus,
                WorkflowDate = DateTime.Now
            });

            var changed = _applicationDbContext.SaveChanges();
            return changed > 0;

        }

    }
}
