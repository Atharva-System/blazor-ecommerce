using BlazorEcommerce.Shared.Product;

namespace BlazorEcommerce.Application.MappingProfıles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.Variants, opt => opt.MapFrom(s => s.Variants))
            .ForMember(d => d.Images, opt => opt.MapFrom(s => s.Images))
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category))
            .ReverseMap();

        CreateMap<ProductVariant, ProductVariantDto>().ReverseMap();
        CreateMap<Image, ImageDto>().ReverseMap();
    }
}
