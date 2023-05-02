using BlazorEcommerce.Application.Contracts.Payment;
using BlazorEcommerce.Application.Model;
using BlazorEcommerce.Infrastructure.Services.PaymentService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorEcommerce.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StripeConfig>(configuration.GetSection("StripeSettings"));

        services.Configure<AppConfig>(configuration.GetSection("AppConfig"));

        services.AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}
