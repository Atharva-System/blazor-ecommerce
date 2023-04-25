using BlazorEcommerce.Shared.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlazorEcommerce.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
