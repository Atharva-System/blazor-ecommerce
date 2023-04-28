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
            var result = await _http.GetFromJsonAsync<IResponse>($"{OrderBaseURL}{orderId}");

            if (result != null && result.Success)
            {
                var resultCast = (DataResponse<OrderDetailsResponse>)result;
                if (resultCast != null)
                {
                    return resultCast.Data;
                }
                else
                {
                    return new OrderDetailsResponse();
                }
            }
            else
            {
                return new OrderDetailsResponse();
            }
        }

        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<IResponse>($"{OrderBaseURL}");

            if (result != null && result.Success)
            {
                var resultCast = (DataResponse<List<OrderOverviewResponse>>)result;
                if (resultCast != null)
                {
                    return resultCast.Data;
                }
                else
                {
                    return new List<OrderOverviewResponse>();
                }
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
                var result = await _http.PostAsync($"{PaymentBaseURL}checkout", null);
                var url = await result.Content.ReadAsStringAsync();
                return url;
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
