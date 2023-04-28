using BlazorEcommerce.Shared.Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorEcommerce.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = Constants.AdminEmail,
                     NormalizedEmail = Constants.AdminEmail.ToUpper(),
                     FirstName = "System",
                     LastName = "Admin",
                     UserName = Constants.AdminEmail,
                     NormalizedUserName = Constants.AdminEmail.ToUpper(),
                     PasswordHash = hasher.HashPassword(null, "Atharva@123"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}
