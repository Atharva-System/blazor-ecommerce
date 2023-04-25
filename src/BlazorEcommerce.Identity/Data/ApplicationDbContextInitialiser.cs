using BlazorEcommerce.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Identity.Data;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private const string AdministratorsRole = "Admin";
    private const string UserRole = "User";

    private const string DefaultPassword = "Atharva@123";

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        await InitialiseWithMigrationsAsync();
    }

    private async Task InitialiseWithDropCreateAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }

    private async Task InitialiseWithMigrationsAsync()
    {
        if (_context.Database.IsSqlServer())
        {
            await _context.Database.MigrateAsync();
        }
        else
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }

    public async Task SeedAsync()
    {
        await SeedIdentityAsync();
    }

    private async Task SeedIdentityAsync()
    {
        // Create roles
        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = AdministratorsRole,
                NormalizedName = AdministratorsRole.ToUpper(),
                Permissions = Permissions.All
            });

        await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = UserRole,
                NormalizedName = UserRole.ToUpper(),
                Permissions =
                    Permissions.ViewUsers |
                    Permissions.Counter
            });

        // Ensure admin role has all permissions
        var adminRole = await _roleManager.FindByNameAsync(AdministratorsRole);
        adminRole!.Permissions = Permissions.All;
        await _roleManager.UpdateAsync(adminRole);

        // Create default admin user
        var adminUserName = "admin@admin.com";
        var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName };

        await _userManager.CreateAsync(adminUser, DefaultPassword);

        adminUser = await _userManager.FindByNameAsync(adminUserName);

        await _userManager.AddToRoleAsync(adminUser!, AdministratorsRole);

        await _context.SaveChangesAsync();
    }
}
