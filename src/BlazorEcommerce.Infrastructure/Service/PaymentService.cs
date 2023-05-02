using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Application.Contracts.Payment;
using BlazorEcommerce.Application.Model;
using BlazorEcommerce.Shared.Cart;
using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Shared.Response.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace BlazorEcommerce.Infrastructure.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICurrentUser _currentUser;
        private readonly StripeConfig _stripeConfig;
        private readonly AppConfig _appConfig;

        private readonly string secret = string.Empty;

        public PaymentService(ICurrentUser currentUser, IOptions<StripeConfig> stripeConfig, IOptions<AppConfig> appConfig)
        {
            _currentUser = currentUser;
            _stripeConfig = stripeConfig.Value;
            _appConfig = appConfig.Value;
            StripeConfiguration.ApiKey = _stripeConfig.ApiKey;
            secret = _stripeConfig.Secret;
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
                SuccessUrl = $"{_appConfig.ClientUrl}order-success",
                CancelUrl = $"{_appConfig.ClientUrl}cart"
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
