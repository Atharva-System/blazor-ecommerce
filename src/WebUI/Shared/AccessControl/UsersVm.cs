﻿namespace BlazorEcommerce.Shared.AccessControl;

public class UsersVm
{
    public IList<UserDto> Users { get; set; } = new List<UserDto>();
}
