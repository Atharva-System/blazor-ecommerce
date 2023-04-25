using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Identity.Data;

public class ApiAuthorizationDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string> where TUser : IdentityUser where TRole : IdentityRole
{
    /// <summary>
    /// Initializes a new instance of <see cref="ApiAuthorizationDbContext{TUser}"/>.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions"/>.</param>
    /// <param name="operationalStoreOptions">The <see cref="IOptions{OperationalStoreOptions}"/>.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApiAuthorizationDbContext(
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
