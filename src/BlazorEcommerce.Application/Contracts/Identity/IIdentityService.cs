using BlazorEcommerce.Shared.User;

namespace BlazorEcommerce.Application.Contracts.Identity;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<Result<string>> CreateUserAsync(
        string userName,
        string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken);

    Task<UserDto> GetUserAsync(string id);

    Task UpdateUserAsync(UserDto updatedUser);

    Task<IResponse> ChangePassword(string userId, string currentPassword, string newPassword);
}
