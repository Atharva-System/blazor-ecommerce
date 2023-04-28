using BlazorEcommerce.Identity.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Persistence.Contexts;

public class UserIdentityDbContextInitialiser
{
    private readonly UserIdentityDbContext _context;

    public UserIdentityDbContextInitialiser(UserIdentityDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        await InitialiseWithMigrationsAsync();
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
}
