using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared.User
{
    public class UserChangePassword
    {
        [Required, StringLength(100, MinimumLength = 6)]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
