using PhoneShop.DAL.Models;
using System.Collections.Generic;

namespace PhoneShop.BLL.Messages
{
    public class SearchPhonesResponse
    {
        public IEnumerable<Phone> Phones{ get; set; }
    }
}