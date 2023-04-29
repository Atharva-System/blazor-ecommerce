using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Application.Contracts.Payment;
using BlazorEcommerce.Shared.Cart;
using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.Response.Concrete;
using Microsoft.AspNetCore.Http;
using Stripe;
using Stripe.Checkout;

namespace BlazorEcommerce.Infrastructure.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICurrentUser _currentUser;

        const string secret = "whsec_97ffacb1f4861c5c1d3b471259cea7c26ba8aaa6b103df80ccd9671e5794ce36";

        public PaymentService(ICurrentUser currentUser)
        {
            StripeConfiguration.ApiKey = "sk_test_51KeFeXSJN18oZA5qDkeNlClNnS5A8xklAv5cvMUJHDRTZTQegBEO36BSpzpBp7gEHGgDUZKNlzmEvHDnhL1CmiRs00bfrcT737";

            _currentUser = currentUser;
        }

        public async Task<IResponse> CreateCheckoutSession(List<CartProductResponse> products)
        {
            var lineItems = new List<SessionLineItemOptions>();
            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string> { product.ImageUrl }
                    }
                },
                Quantity = product.Quantity
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _currentUser.UserEmail,
                ShippingAddressCollection =
                    new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> { "US" }
                    },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7018/order-success",
                CancelUrl = "https://localhost:7018/cart"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return new DataResponse<string>(session.Url, HttpStatusCodes.Accepted);
        }

        public async Task<IResponse> FulfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        request.Headers["Stripe-Signature"],
                        secret
                    );

                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    return new DataResponse<string?>(null);
                }

                return new DataResponse<string?>(null, HttpStatusCodes.InternalServerError, stripeEvent.StripeResponse.Content, false);
            }
            catch (StripeException e)
            {
                return new DataResponse<string?>(null, HttpStatusCodes.InternalServerError, e.Message, false);
            }   
        }
    }
}
