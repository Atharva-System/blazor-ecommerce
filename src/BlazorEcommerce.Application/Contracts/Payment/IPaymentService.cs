using BlazorEcommerce.Shared.Cart;
using Microsoft.AspNetCore.Http;

namespace BlazorEcommerce.Application.Contracts.Payment
{
    public interface IPaymentService
    {
        Task<IResponse> CreateCheckoutSession(List<CartProductResponse> products);
        Task<IResponse> FulfillOrder(HttpRequest request);
    }
}
