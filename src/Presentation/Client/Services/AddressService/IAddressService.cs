namespace BlazorEcommerce.Client.Services.AddressService
{
    public interface IAddressService
    {
        Task<AddressDto> GetAddress();
        Task<AddressDto> AddOrUpdateAddress(AddressDto address);
    }
}
