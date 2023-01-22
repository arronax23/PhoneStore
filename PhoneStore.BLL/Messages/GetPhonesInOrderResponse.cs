using PhoneStore.DAL.Models;
using System.Collections.Generic;

namespace PhoneStore.BLL.Messages
{
    public class GetPhonesInOrderResponse
    {
        public IEnumerable<Phone> Phones { get; set; }
    }
}