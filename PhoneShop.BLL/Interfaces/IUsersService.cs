using System.Threading.Tasks;
using PhoneShop.BLL.Messages;

namespace PhoneShop.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
    }
}