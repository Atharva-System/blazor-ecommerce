using BlazorEcommerce.Application.UnitOfWork;
using BlazorEcommerce.Persistence.Contexts;
using BlazorEcommerce.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorEcommerce.Persistence;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<PersistenceDataContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<PersistenceDbContextInitialiser>();

        services.AddScoped<IQueryUnitOfWork, QueryUnitOfWork>();
        services.AddScoped(typeof(ICommandUnitOfWork<>), typeof(CommandUnitOfWork<>));

        services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
        services.AddScoped<IAddressCommandRepository, AddressCommandRepository>();
        services.AddScoped<ICartItemCommandRepository, CartItemCommandRepository>();
        services.AddScoped<IImageCommandRepository, ImageCommandRepository>();
        services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
        services.AddScoped<IOrderItemCommandRepository, OrderItemCommandRepository>();
        services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        services.AddScoped<IProductTypeCommandRepository, ProductTypeCommandRepository>();
        services.AddScoped<IProductVariantCommandRepository, ProductVariantCommandRepository>();

        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<IAddressQueryRepository, AddressQueryRepository>();
        services.AddScoped<ICartItemQueryRepository, CartItemQueryRepository>();
        services.AddScoped<IImageQueryRepository, ImageQueryRepository>();
        services.AddScoped<IOrderItemQueryRepository, OrderItemQueryRepository>();
        services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
        services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
        services.AddScoped<IProductTypeQueryRepository, ProductTypeQueryRepository>();
        services.AddScoped<IProductVariantQueryRepository, ProductVariantQueryRepository>();

        return services;
    }
}
