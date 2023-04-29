using Ardalis.GuardClauses;
using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Shared.Common;
using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.Response.Concrete;
using BlazorEcommerce.Shared.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Identity.Service;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u =>
            u.Id == userId);

        return user.UserName!;
    }

    public async Task<Result<string>> CreateUserAsync(
        string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return Result<string>.Success(user.Id);
        }

        return Result<string>.Failure(result.Errors.Select(e => e.Description));
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u =>
            u.Id == userId);

        if (user != null)
        {
            return await DeleteUserAsync(user);
        }

        return Result.Success();
    }

    private async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Result.Success();
        }

        return Result.Failure(result.Errors.Select(e => e.Description));
    }

    public async Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .OrderBy(r => r.UserName)
            .Select(u => new UserDto(u.Id, u.UserName ?? string.Empty, u.Email ?? string.Empty, u.FirstName ?? string.Empty, u.LastName ?? string.Empty))
            .ToListAsync(cancellationToken);
    }

    public async Task<UserDto> GetUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        Guard.Against.NotFound(id, user);

        var result = new UserDto(user.Id, user.UserName ?? string.Empty, user.Email ?? string.Empty, user.FirstName ?? string.Empty, user.LastName ?? string.Empty);

        var roles = await _userManager.GetRolesAsync(user);

        result.Roles.AddRange(roles);

        return result;
    }

    public async Task UpdateUserAsync(UserDto updatedUser)
    {
        var user = await _userManager.FindByIdAsync(updatedUser.Id);

        Guard.Against.NotFound(updatedUser.Id, user);

        user.UserName = updatedUser.UserName;
        user.Email = updatedUser.Email;

        await _userManager.UpdateAsync(user);

        var currentRoles = await _userManager.GetRolesAsync(user);
        var addedRoles = updatedUser.Roles.Except(currentRoles);
        var removedRoles = currentRoles.Except(updatedUser.Roles);

        if (addedRoles.Any())
        {
            await _userManager.AddToRolesAsync(user, addedRoles);
        }

        if (removedRoles.Any())
        {
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }
    }

    public async Task<IResponse> ChangePassword(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);

        Guard.Against.NotFound(userId, user);

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result.Succeeded)
        {
            return new DataResponse<bool>(true, HttpStatusCodes.Accepted,Messages.PasswordChangedSuccess);
        }
        else
        {
            List<string> str = new List<string>();
            foreach (var err in result.Errors)
            {
                str.Add(err.Description);
            }

            return new DataResponse<string?>(null, HttpStatusCodes.BadRequest, str, false);

        }
    }
}
