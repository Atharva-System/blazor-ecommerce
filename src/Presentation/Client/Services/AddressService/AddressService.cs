namespace BlazorEcommerce.Client.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _http;
        private const string AddressBaseURL = "api/address/";

        public AddressService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AddressDto> AddOrUpdateAddress(AddressDto address)
        {
            var response = await _http.PostAsJsonAsync($"{AddressBaseURL}", address);
            var result = response.Content
                .ReadFromJsonAsync<ApiResponse<AddressDto>>().Result;

            if (result != null && result.Success)
            {
                return await GetAddress();
            }
            else
            {
                return new AddressDto();
            }
        }

        public async Task<AddressDto> GetAddress()
        {
            var response = await _http
                .GetFromJsonAsync<ApiResponse<AddressDto>>($"{AddressBaseURL}");

            if (response != null && response.Success)
            {
                return response.Data;
            }
            else
            {
                return new AddressDto();
            }
        }
    }
}
