using BlazorEcommerce.Shared.User;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorEcommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private const string AuthBaseURL = "api/auth/";
        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<IResponse> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync($"{AuthBaseURL}change-password", request);
            return await result.Content.ReadFromJsonAsync<IResponse>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<IResponse> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync($"{AuthBaseURL}login", request);
            return await result.Content.ReadFromJsonAsync<IResponse>();
        }

        public async Task<IResponse> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync($"{AuthBaseURL}register", request);
            return await result.Content.ReadFromJsonAsync<IResponse>();
        }
    }
}
