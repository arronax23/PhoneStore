using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.DAL.Data;
using PhoneShop.DAL.Models;

namespace PhoneShop.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {

        private ApplicationDbContext applicationDbContext;

        public PhonesController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Phone> GetAllPhones()
        {
            
            return applicationDbContext.Phones.AsEnumerable();
        }
    }
}