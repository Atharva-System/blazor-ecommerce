using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Contracts.Identity;

public interface IAuthService
{
    Task<IResponse> Register(UserRegister request);
    Task<IResponse> Login(UserLogin request);
    
}
