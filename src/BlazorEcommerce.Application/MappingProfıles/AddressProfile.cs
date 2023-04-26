using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Application.MappingProfıles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>();
    }
}
