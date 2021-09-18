using System.Threading.Tasks;
using PhoneShop.BLL.Messages;

namespace PhoneShop.BLL.Interfaces
{
    public interface IUsersService
    {
        Task RegisterUser(RegisterUserRequest request);
    }
}