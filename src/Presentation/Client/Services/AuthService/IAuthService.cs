using BlazorEcommerce.Shared.Auth;
using BlazorEcommerce.Shared.User;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> Register(UserRegister request);
        Task<ApiResponse<AuthResponseDto>> Login(UserLogin request);
        Task<string> RefreshToken();
        Task<bool> IsUserAuthenticated();
    }
}
