using PhoneShop.BLL.Messages;
using System.Threading.Tasks;

namespace PhoneShop.BLL.Interfaces
{
    public interface IOrdersService
    {
        GetOrdersByCustomerIdResponse GetOrdersByCustomerId(GetOrdersByCustomerIdRequest request);
        Task<bool> ChangeOrderStatus(ChangeOrderStatusRequest request);
    }
}