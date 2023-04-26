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
}
