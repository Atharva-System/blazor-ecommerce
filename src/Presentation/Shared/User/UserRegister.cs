using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.User
{
    public class UserRegister
    {
        [Required, StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;


        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
