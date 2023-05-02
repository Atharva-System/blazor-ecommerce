using BlazorEcommerce.Shared.Response.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorEcommerce.Client.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;
        private const string OrderBaseURL = "api/order/";
        private const string PaymentBaseURL = "api/payment/";

        public OrderService(HttpClient http,
            AuthenticationStateProvider authStateProvider,
            NavigationManager navigationManager)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ApiResponse<OrderDetailsResponse>>($"{OrderBaseURL}{orderId}");

            if (result != null && result.Success)
            {
                return result.Data;
            }
            else
            {
                return new OrderDetailsResponse();
            }
        }

        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ApiResponse<List<OrderOverviewResponse>>>($"{OrderBaseURL}");

            if (result != null && result.Success)
            {
                return result.Data;
            }
            else
            {
                return new List<OrderOverviewResponse>();
            }
        }

        public async Task<string> PlaceOrder()
        {
            if (await IsUserAuthenticated())
            {
                var response = await _http.PostAsync($"{PaymentBaseURL}checkout", null);
                var result = response.Content
               .ReadFromJsonAsync<ApiResponse<string>>().Result;

                if (result != null && result.Success)
                {
                    return result.Data;
                }
                else
                {
                    return "login";
                }
            }
            else
            {
                return "login";
            }
        }
        private async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }
    }
}
