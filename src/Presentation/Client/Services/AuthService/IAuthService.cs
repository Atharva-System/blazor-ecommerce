using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.User;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<DataResponse<string>> Register(UserRegister request);
        Task<DataResponse<string>> Login(UserLogin request);
        Task<DataResponse<string>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
    }
}
