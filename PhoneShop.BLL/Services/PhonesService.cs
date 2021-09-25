using PhoneShop.BLL.Interfaces;
using PhoneShop.BLL.Messages;
using PhoneShop.DAL.Data;
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
            var phone = _applicationDbContext.Phones.FirstOrDefault(p => p.PhoneId == request.PhoneId);
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
            var phoneToBeRemoved = _applicationDbContext.Phones.FirstOrDefault(p => p.PhoneId == request.PhoneId);
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
    }
}
