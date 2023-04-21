using Stripe.Checkout;
using Stripe;

namespace BlazorEcommerce.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;

        const string secret = "whsec_97ffacb1f4861c5c1d3b471259cea7c26ba8aaa6b103df80ccd9671e5794ce36";

        public PaymentService(ICartService cartService,
            IAuthService authService,
            IOrderService orderService)
        {
            StripeConfiguration.ApiKey = "sk_test_51KeFeXSJN18oZA5qDkeNlClNnS5A8xklAv5cvMUJHDRTZTQegBEO36BSpzpBp7gEHGgDUZKNlzmEvHDnhL1CmiRs00bfrcT737";

            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }

        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
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
                CustomerEmail = _authService.GetUserEmail(),
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
            return session;
        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
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
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrder(user.Id);
                }

                return new ServiceResponse<bool> { Data = true };
            }
            catch (StripeException e)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
            }
        }
    }
}
