namespace BlazorEcommerce.Application.Contracts.Identity;

public interface ICurrentUser
{
    string? UserId { get; }

    string? UserEmail { get; }

    bool UserIsInRole(string roleName);

    bool IsUserAuthenticated { get; }
}
