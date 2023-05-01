using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.User;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Register(UserRegister request);
        Task<ApiResponse<string>> Login(UserLogin request);
        Task<bool> IsUserAuthenticated();
    }
}
