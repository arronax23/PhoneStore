using PhoneShop.BLL.Messages;

namespace PhoneShop.BLL.Interfaces
{
    public interface IPhonesService
    {
        GetAllPhonesResponse GetAllPhones();

        GetPhonesForOnePageResponse GetPhonesForOnePage(GetPhonesForOnePageRequest request);

        GetNumberOfPagesInPhoneListResponse GetNumberOfPagesInPhoneList();
        GetPhoneByIdResponse GetPhoneById(GetPhoneByIdRequest request);
        bool CreatePhone(SavePhoneRequest request);
        bool DeletePhoneById(DeletePhoneByIdRequest request);
        bool UpdatePhone(UpdatePhoneRequest request);
        bool AddPhoneToShoppingCart(AddPhoneToShoppingCardRequest request);
        IsPhoneInShoppingCartResponse IsPhoneInShoppingCart(IsPhoneInShoppingCartRequest request);
        bool RemovePhoneFromShoppingCart(RemovePhoneFromShoppingCartRequest request);
        GetPhonesInOrderResponse GetPhonesInOrder(GetPhonesInOrderRequest request);

        SearchPhonesResponse SearchPhones(SearchPhonesRequest request);
    }
}