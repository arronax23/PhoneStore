using PhoneShop.DAL.Models;
using System.Collections.Generic;

namespace PhoneShop.BLL.Messages
{
    public class GetPhonesForOnePageResponse
    {
        public IEnumerable<Phone> Phones { get; set; }
    }
}