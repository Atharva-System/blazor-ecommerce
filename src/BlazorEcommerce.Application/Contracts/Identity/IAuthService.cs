using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.Contracts.Identity;

public interface IAuthService
{
    Task<ServiceResponse<string>> Register(UserRegister request);
    Task<ServiceResponse<string>> Login(UserLogin request);
    
}
