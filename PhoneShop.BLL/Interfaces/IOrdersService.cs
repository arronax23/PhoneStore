using PhoneShop.BLL.Messages;

namespace PhoneShop.BLL.Interfaces
{
    public interface IOrdersService
    {
        GetOrdersByCustomerIdResponse GetOrdersByCustomerId(GetOrdersByCustomerIdRequest request);
        bool ChangeOrderStatus(ChangeOrderStatusRequest request);
    }
}