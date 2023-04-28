using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Persistence.Contexts;

public class PersistenceDbContextInitialiser
{
    private readonly PersistenceDataContext _context;

    public PersistenceDbContextInitialiser(PersistenceDataContext context)
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
