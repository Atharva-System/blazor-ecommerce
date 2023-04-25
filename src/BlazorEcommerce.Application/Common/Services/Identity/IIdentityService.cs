﻿using BlazorEcommerce.Shared.AccessControl;
using BlazorEcommerce.Shared.Authorization;

namespace BlazorEcommerce.Application.Common.Services.Identity;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<Result<string>> CreateUserAsync(
        string userName,
        string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<IList<RoleDto>> GetRolesAsync(CancellationToken cancellationToken);

    Task UpdateRolePermissionsAsync(string roleId, Permissions permissions);

    Task<IList<UserDto>> GetUsersAsync(CancellationToken cancellationToken);

    Task<UserDto> GetUserAsync(string id);

    Task UpdateUserAsync(UserDto updatedUser);

    Task CreateRoleAsync(RoleDto newRole);

    Task UpdateRoleAsync(RoleDto updatedRole);

    Task DeleteRoleAsync(string roleId);
}
