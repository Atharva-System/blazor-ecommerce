using BlazorEcommerce.Application.Contracts.Identity;

namespace BlazorEcommerce.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        private readonly ICurrentUser _authService;

        public AddressService(DataContext context, ICurrentUser authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
        {
            var response = new ServiceResponse<Address>();
            var dbAddress = (await GetAddress()).Data;
            if (dbAddress == null)
            {
                address.UserId = _authService.UserId;
                _context.Addresses.Add(address);
                response.Data = address;
            }
            else
            {
                dbAddress.FirstName = address.FirstName;
                dbAddress.LastName = address.LastName;
                dbAddress.State = address.State;
                dbAddress.Country = address.Country;
                dbAddress.City = address.City;
                dbAddress.Zip = address.Zip;
                dbAddress.Street = address.Street;
                response.Data = dbAddress;
            }

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            string userId = _authService.UserId;
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId);
            return new ServiceResponse<Address> { Data = address };
        }
    }
}
