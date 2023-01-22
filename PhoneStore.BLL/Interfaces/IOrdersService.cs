using PhoneStore.BLL.Messages;
using System.Threading.Tasks;

namespace PhoneStore.BLL.Interfaces
{
    public interface IOrdersService
    {
        GetOrdersByCustomerIdResponse GetOrdersByCustomerId(GetOrdersByCustomerIdRequest request);
        Task<bool> ChangeOrderStatus(ChangeOrderStatusRequest request);
    }
}