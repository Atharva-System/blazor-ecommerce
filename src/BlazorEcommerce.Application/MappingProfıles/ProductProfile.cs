using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.MappingProfıles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductVariant, ProductVariantDto>().ReverseMap();
    }
}
