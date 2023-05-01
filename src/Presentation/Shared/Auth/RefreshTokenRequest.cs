using System.Text.Json.Serialization;

namespace BlazorEcommerce.Shared.User
{
    public class RefreshTokenRequest
    {
        [JsonIgnore]
        public string? RefreshToken { get; set; } = string.Empty;

        public string CurrentToken { get; set; } = string.Empty;
    }
}
