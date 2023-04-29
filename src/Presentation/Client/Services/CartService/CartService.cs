using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthService _authService;
        private const string CartBaseURL = "api/cart/";

        public CartService(ILocalStorageService localStorage, HttpClient http,
            IAuthService authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItemDto cartItem)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync($"{CartBaseURL}add", cartItem);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cart == null)
                {
                    cart = new List<CartItemDto>();
                }

                var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId &&
                    x.ProductTypeId == cartItem.ProductTypeId);
                if (sameItem == null)
                {
                    cart.Add(cartItem);
                }
                else
                {
                    sameItem.Quantity += cartItem.Quantity;
                }

                await _localStorage.SetItemAsync("cart", cart);
            }
            await GetCartItemsCount();
        }

        public async Task GetCartItemsCount()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync<ApiResponse<int>>($"{CartBaseURL}count");
                if (result != null && result.Success)
                {
                    var count = result.Data;

                    await _localStorage.SetItemAsync<int>("cartItemsCount", count);
                }
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);
            }

            OnChange.Invoke();
        }

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync<ApiResponse<List<CartProductResponse>>>($"{CartBaseURL}");
                if (result != null && result.Success)
                {
                    return result.Data;
                }

                return new List<CartProductResponse>();
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cartItems == null)
                    return new List<CartProductResponse>();
                var response = await _http.PostAsJsonAsync($"{CartBaseURL}products", cartItems);
                var cartProducts = 
                    await response.Content.ReadFromJsonAsync<ApiResponse<List<CartProductResponse>>>();

                if (cartProducts != null && cartProducts.Success)
                {
                    return cartProducts.Data;

                }

                return new List<CartProductResponse>();
            }

        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.DeleteAsync($"{CartBaseURL}{productId}/{productTypeId}");
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == productId
                    && x.ProductTypeId == productTypeId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
            if (localCart == null)
            {
                return;
            }

            await _http.PostAsJsonAsync($"{CartBaseURL}", localCart);

            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");
            }
        }

        public async Task UpdateQuantity(CartProductResponse product)
        {
            if (await _authService.IsUserAuthenticated())
            {
                var request = new CartItemDto
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductTypeId = product.ProductTypeId
                };
                await _http.PutAsJsonAsync($"{CartBaseURL}update-quantity", request);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItemDto>>("cart");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == product.ProductId
                    && x.ProductTypeId == product.ProductTypeId);
                if (cartItem != null)
                {
                    cartItem.Quantity = product.Quantity;
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }
    }
}
