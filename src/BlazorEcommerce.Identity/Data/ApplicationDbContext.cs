using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BlazorEcommerce.Identity.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser, ApplicationRole>
{
    public ApplicationDbContext(
        DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.ApplyConfigurationsFromAssembly(
        //    Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
