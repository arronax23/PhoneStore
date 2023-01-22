using PhoneStore.DAL.Models;
using System.Collections.Generic;

namespace PhoneStore.BLL.Messages
{
    public class SearchPhonesResponse
    {
        public IEnumerable<Phone> Phones{ get; set; }
        //public int NumberOfPages{ get; set; }
    }
}