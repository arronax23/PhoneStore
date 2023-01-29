using System.Threading.Tasks;
using PhoneStore.BLL.Messages;

namespace PhoneStore.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task Logout();
        Task<GetCustomerIdByUsernameResponse> GetCustomerIdByUsername(GetCustomerIdByUsernameRequest request);
    }
}