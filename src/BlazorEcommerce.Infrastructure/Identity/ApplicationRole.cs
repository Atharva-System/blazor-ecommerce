using BlazorEcommerce.Shared.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlazorEcommerce.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
