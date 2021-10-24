using PhoneShop.BLL.Messages;

namespace PhoneShop.BLL.Interfaces
{
    public interface IPhonesService
    {
        GetAllPhonesResponse GetAllPhones();

        GetPhonesForOnePageResponse GetPhonesForOnePage(GetPhonesForOnePageRequest request);

        GetNumberOfPagesInPhoneListResponse GetNumberOfPagesInPhoneList();
        GetPhoneByIdResponse GetPhoneById(GetPhoneByIdRequest request);
        void CreatePhone(SavePhoneRequest request);
        void DeletePhoneById(DeletePhoneByIdRequest request);
        void UpdatePhone(UpdatePhoneRequest request);
        void AddPhoneToShoppingCart(AddPhoneToShoppingCardRequest request);
        IsPhoneInShoppingCartResponse IsPhoneInShoppingCart(IsPhoneInShoppingCartRequest request);
        void RemovePhoneFromShoppingCart(RemovePhoneFromShoppingCartRequest request);
        GetPhonesInOrderResponse GetPhonesInOrder(GetPhonesInOrderRequest request);

        SearchPhonesResponse SearchPhones(SearchPhonesRequest request);
    }
}