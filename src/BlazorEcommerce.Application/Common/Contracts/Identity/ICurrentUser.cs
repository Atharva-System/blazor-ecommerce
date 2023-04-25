namespace BlazorEcommerce.Application.Common.Contracts.Identity;

public interface ICurrentUser
{
    string? UserId { get; }
}
