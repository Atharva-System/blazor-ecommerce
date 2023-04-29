namespace BlazorEcommerce.Shared.User;

public class UserDto
{
    public UserDto() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

    public UserDto(string id, string userName, string email, string firstName, string lastName)
    {
        Id = id;
        UserName = userName;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public List<string> Roles { get; set; } = new();
}
