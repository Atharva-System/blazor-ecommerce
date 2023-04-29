using BlazorEcommerce.Application.Contracts.Identity;
using System.Security.Claims;

namespace BlazorEcommerce.Server.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?
                            .User?
                            .FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserEmail => _httpContextAccessor.HttpContext?
                           .User?
                           .FindFirstValue(ClaimTypes.Name);

    public bool IsUserAuthenticated
    {
        get
        {
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null && _httpContextAccessor.HttpContext.User.Identity != null)
            {
                return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
            return false;
        }
       
    }

    public bool UserIsInRole(string roleName)
    {
        if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
        {
            return _httpContextAccessor.HttpContext.User.IsInRole(roleName);
        }
        return false;
    }
}
