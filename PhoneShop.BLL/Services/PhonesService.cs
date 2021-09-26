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
    public class PhonesService : IPhonesService
    {
        private ApplicationDbContext _applicationDbContext;
        public PhonesService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public GetAllPhonesResponse GetAllPhones()
        {
            var response = new GetAllPhonesResponse() { Phones = _applicationDbContext.Phones.AsEnumerable() };
            return response;
        }
        public GetPhoneByIdResponse GetPhoneById(GetPhoneByIdRequest request)
        {
            var phone = _applicationDbContext.Phones.SingleOrDefault(p => p.PhoneId == request.PhoneId);
            if (phone == null)
                throw new Exception("No phone was found.");
            var response = new GetPhoneByIdResponse() { Phone = phone };
            return response;
        }
        public void SavePhone(SavePhoneRequest request)
        {
            _applicationDbContext.Phones.Add(request.Phone);
            _applicationDbContext.SaveChanges();
        }

        public void DeletePhoneById(DeletePhoneByIdRequest request)
        {
            var phoneToBeRemoved = _applicationDbContext.Phones.SingleOrDefault(p => p.PhoneId == request.PhoneId);
            if (phoneToBeRemoved == null)
                throw new Exception("No phone was found.");

            _applicationDbContext.Phones.Remove(phoneToBeRemoved);
            _applicationDbContext.SaveChanges();
        }

        public void UpdatePhone(UpdatePhoneRequest request)
        {
            _applicationDbContext.Phones.Update(request.Phone);
            _applicationDbContext.SaveChanges();
   
        }

        public void AddPhoneToShoppingCard(AddPhoneToShoppingCardRequest request)
        {
            var customer = _applicationDbContext.Customers.SingleOrDefault(c => c.CustomerId == request.CustomerId);
            if (customer == null)
                throw new Exception("No customer was found.");
            var phone = _applicationDbContext.Phones.SingleOrDefault(p => p.PhoneId == request.PhoneId);
            if (phone == null)
                throw new Exception("No phone was found.");

            var currentOrder = _applicationDbContext.Orders.
                Where(o => o.Status == OrderStatus.Open).
                OrderByDescending(o => o.CreatedDate).
                FirstOrDefault();

            if (currentOrder == null)
            {
                var phoneOrder = new PhoneOrder() { Phone = phone };
                currentOrder = new Order()
                {
                    Customer = customer,
                    PhoneOrder = new List<PhoneOrder>() { phoneOrder },
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                var orderStatuWorkflow = new OrderStatusWorkflow()
                {
                    Order = currentOrder,
                    Status = OrderStatus.Open,
                    WorkflowDate = DateTime.Now
                };

                _applicationDbContext.Orders.Add(currentOrder);
                _applicationDbContext.OrderStatusWorkflows.Add(orderStatuWorkflow);
            }
            else
            {
                var phoneOrder = new PhoneOrder() { Phone = phone };
                currentOrder.PhoneOrder.Add(phoneOrder);
                currentOrder.ModifiedDate = DateTime.Now;
            }

            _applicationDbContext.SaveChanges();
        }
    }
}
