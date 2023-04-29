using BlazorEcommerce.Shared.Cart;

namespace BlazorEcommerce.Application.MappingProfıles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemDto>().ReverseMap();
    }
}
