using PhoneStore.BLL.Messages;
using System.Threading.Tasks;

namespace PhoneStore.BLL.Interfaces
{
    public interface IPhonesService
    {
        Task<bool> AddPhoneToShoppingCart(AddPhoneToShoppingCardRequest request);
        Task<bool> CreatePhone(SavePhoneRequest request);
        Task<bool> DeletePhoneById(DeletePhoneByIdRequest request);
        GetAllPhonesResponse GetAllPhones();
        GetNumberOfPagesInPhoneListResponse GetNumberOfPagesInPhoneList();
        GetPhoneByIdResponse GetPhoneById(GetPhoneByIdRequest request);
        GetPhonesForOnePageResponse GetPhonesForOnePage(GetPhonesForOnePageRequest request);
        GetPhonesInOrderResponse GetPhonesInOrder(GetPhonesInOrderRequest request);
        Task<IsPhoneInShoppingCartResponse> IsPhoneInShoppingCart(IsPhoneInShoppingCartRequest request);
        Task<bool> RemovePhoneFromShoppingCart(RemovePhoneFromShoppingCartRequest request);
        SearchPhonesResponse SearchPhones(SearchPhonesRequest request);
        Task<bool> UpdatePhone(UpdatePhoneRequest request);
    }
}