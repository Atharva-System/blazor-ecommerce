using BlazorEcommerce.Shared.Response.Abstract;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<IResponse> Register(UserRegister request);
        Task<IResponse> Login(UserLogin request);
        Task<IResponse> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
    }
}
