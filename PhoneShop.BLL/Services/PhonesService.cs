using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Data;
using PhoneShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneShop.BLL.Services
{
    public class PhonesService : IPhonesService
    {
        private ApplicationDbContext _applicationDbContext;
        private IConfiguration _configuration;

        private int numberOfPhonesPerPage;

        public PhonesService(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
            numberOfPhonesPerPage = int.Parse(_configuration["Config:NumberOfPhonesPerPage"]);
        }

        public GetAllPhonesResponse GetAllPhones()
        {
            var response = new GetAllPhonesResponse() { Phones = _applicationDbContext.Phones.AsEnumerable() };
            return response;
        }

        public GetPhonesForOnePageResponse GetPhonesForOnePage(GetPhonesForOnePageRequest request)
        {
            var response = new GetPhonesForOnePageResponse()
            {
                Phones = _applicationDbContext.Phones
                .OrderBy(p => p.Brand)
                .Skip(numberOfPhonesPerPage * (request.PageNumber - 1))
                .Take(numberOfPhonesPerPage)
                .AsEnumerable()
            };
            return response;
        }

        public SearchPhonesResponse SearchPhones(SearchPhonesRequest request)
        {
            var searchTextUpper = request.SearchText.ToUpper();

            var phones =
                _applicationDbContext.Phones
                    .AsEnumerable()
                    .Select(phone => new { Phone = phone, PhoneName = $"{phone.Brand} {phone.Model}" })
                    .Where(phonesWithName =>
                        phonesWithName.PhoneName.ToUpper().StartsWith(searchTextUpper)
                        || phonesWithName.Phone.Model.ToUpper().StartsWith(searchTextUpper)
                        || phonesWithName.Phone.Brand.ToUpper().StartsWith(searchTextUpper))
                    .Select(phonesWithName => phonesWithName.Phone);


            var response = new SearchPhonesResponse()
            {
                //Phones = _applicationDbContext.Phones
                //            .Where(phone => phone.Model.StartsWith(request.SearchText) || phone.Brand.StartsWith(request.SearchText))
                Phones = phones

            };
            //response.NumberOfPages = (int)Math.Ceiling((double)response.Phones.ToList().Count / numberOfPhonesPerPage);

            return response;
        }

        public GetNumberOfPagesInPhoneListResponse GetNumberOfPagesInPhoneList()
        {
            double numberOfAllPhones = _applicationDbContext.Phones.Count();
            double numberOfPages = Math.Ceiling(numberOfAllPhones / numberOfPhonesPerPage);
            int numberOfPagesInt = (int)numberOfPages;

            var response = new GetNumberOfPagesInPhoneListResponse()
            {
                NumberOfPages = numberOfPagesInt
            };

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
        public async Task<bool> CreatePhone(SavePhoneRequest request)
        {
            await _applicationDbContext.Phones.AddAsync(request.Phone);
            var isCreated = await _applicationDbContext.SaveChangesAsync() > 0;
            return isCreated;
        }

        public async Task<bool> DeletePhoneById(DeletePhoneByIdRequest request)
        {
            var phoneToBeRemoved = await _applicationDbContext.Phones.SingleOrDefaultAsync(p => p.PhoneId == request.PhoneId);
            if (phoneToBeRemoved == null)
                throw new Exception("No phone was found.");

            _applicationDbContext.Phones.Remove(phoneToBeRemoved);
            var isDeleted = await _applicationDbContext.SaveChangesAsync() > 0;
            return isDeleted;
        }

        public async Task<bool> UpdatePhone(UpdatePhoneRequest request)
        {
            _applicationDbContext.Phones.Update(request.Phone);
            var isUpdated = await _applicationDbContext.SaveChangesAsync() > 0;
            return isUpdated;

        }

        public async Task<bool> AddPhoneToShoppingCart(AddPhoneToShoppingCardRequest request)
        {
            var customer = await _applicationDbContext.Customers.SingleOrDefaultAsync(c => c.CustomerId == request.CustomerId);
            if (customer == null)
                throw new Exception("No customer was found.");
            var phone = await _applicationDbContext.Phones.SingleOrDefaultAsync(p => p.PhoneId == request.PhoneId);
            if (phone == null)
                throw new Exception("No phone was found.");

            var currentOrder = await _applicationDbContext.Orders
                .Include(o => o.PhoneOrder)
                .SingleOrDefaultAsync(o => o.Status == OrderStatus.Open && o.CustomerId == request.CustomerId);

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

                var orderStatusWorkflow = new OrderStatusWorkflow()
                {
                    Order = currentOrder,
                    Status = OrderStatus.Open,
                    WorkflowDate = DateTime.Now
                };

                await _applicationDbContext.Orders.AddAsync(currentOrder);
                await _applicationDbContext.OrderStatusWorkflows.AddAsync(orderStatusWorkflow);
            }
            else
            {
                var phoneOrder = new PhoneOrder() { Phone = phone };
                currentOrder.PhoneOrder.Add(phoneOrder);
                currentOrder.ModifiedDate = DateTime.Now;
            }

            var isAdded = await _applicationDbContext.SaveChangesAsync() > 0;
            return isAdded;
        }
        public async Task<IsPhoneInShoppingCartResponse> IsPhoneInShoppingCart(IsPhoneInShoppingCartRequest request)
        {
            var resposne = new IsPhoneInShoppingCartResponse();

            var currentOrder = await _applicationDbContext.Orders
                .Include(o => o.PhoneOrder)
                .SingleOrDefaultAsync(o => o.Status == OrderStatus.Open && o.CustomerId == request.CustomerId);

            if (currentOrder == null)
            {
                resposne.IsPhoneInShoppingCart = false;
            }
            else
            {
                var phoneOrder = await _applicationDbContext.PhoneOrders
                    .SingleOrDefaultAsync(po => po.OrderId == currentOrder.OrderId && po.PhoneId == request.PhoneId);

                if (phoneOrder == null)
                    resposne.IsPhoneInShoppingCart = false;
                else
                    resposne.IsPhoneInShoppingCart = true;
            }

            return resposne;
        }

        public async Task<bool> RemovePhoneFromShoppingCart(RemovePhoneFromShoppingCartRequest request)
        {
            var currentOrder = await _applicationDbContext.Orders
            .Include(o => o.PhoneOrder)
            .SingleOrDefaultAsync(o => o.Status == OrderStatus.Open && o.CustomerId == request.CustomerId);

            if (currentOrder == null)
                throw new Exception("No order was found");

            var phoneOrder = currentOrder.PhoneOrder.SingleOrDefault(po => po.PhoneId == request.PhoneId);

            if (phoneOrder == null)
                throw new Exception("No phone was found in open order");

            _applicationDbContext.PhoneOrders.Remove(phoneOrder);
            var isDeleted = await _applicationDbContext.SaveChangesAsync() > 0;
            return isDeleted;
        }

        public GetPhonesInOrderResponse GetPhonesInOrder(GetPhonesInOrderRequest request)
        {
            var getPhonesInOrderResponse = new GetPhonesInOrderResponse();

            var phones = _applicationDbContext.PhoneOrders
                .Include(po => po.Phone)
                .Where(po => po.OrderId == request.OrderId)
                .Select(po => new Phone()
                {
                    PhoneId = po.Phone.PhoneId,
                    Brand = po.Phone.Brand,
                    Model = po.Phone.Model,
                    RAM = po.Phone.RAM,
                    Camera = po.Phone.Camera,
                    Memory = po.Phone.Memory,
                    Color = po.Phone.Color,
                    ImageUrl = po.Phone.ImageUrl,
                    OS = po.Phone.OS,
                    Price = po.Phone.Price
                })
                .AsEnumerable();

            getPhonesInOrderResponse.Phones = phones;

            return getPhonesInOrderResponse;

        }

    }
}
