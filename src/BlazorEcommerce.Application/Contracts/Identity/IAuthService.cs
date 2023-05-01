using BlazorEcommerce.Shared.User;

namespace BlazorEcommerce.Application.Contracts.Identity;

public interface IAuthService
{
    Task<IResponse> Register(UserRegister request);
    Task<IResponse> Login(UserLogin request);
    Task<IResponse> RefreshToken(RefreshTokenRequest request);
}
