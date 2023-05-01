using BlazorEcommerce.Shared.User;

namespace BlazorEcommerce.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private const string UserBaseURL = "api/user/";

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<string>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync($"{UserBaseURL}change-password", request);
            return await result.Content.ReadFromJsonAsync<ApiResponse<string>>();
        }
    }
}
