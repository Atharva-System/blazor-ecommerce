global using BlazorEcommerce.Client.Services;
global using BlazorEcommerce.Client.Services.AddressService;
global using BlazorEcommerce.Client.Services.AuthService;
global using BlazorEcommerce.Client.Services.CartService;
global using BlazorEcommerce.Client.Services.CategoryService;
global using BlazorEcommerce.Client.Services.OrderService;
global using BlazorEcommerce.Client.Services.ProductService;
global using BlazorEcommerce.Client.Services.ProductTypeService;
global using BlazorEcommerce.Client.Services.UserService;
global using BlazorEcommerce.Shared;
global using BlazorEcommerce.Shared.Cart;
global using BlazorEcommerce.Shared.Order;
global using BlazorEcommerce.Shared.Product;
global using BlazorEcommerce.Shared.Response.Concrete;
global using System.Net.Http.Json;
using BlazorEcommerce.Client;
using BlazorEcommerce.Client.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddLocalization();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var apiUrl = builder.Configuration.GetValue<string>("AppConfig:ApiUrl");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(!string.IsNullOrEmpty(apiUrl) ? apiUrl : builder.HostEnvironment.BaseAddress),
}.EnableIntercept(sp));

builder.Services.AddHttpClientInterceptor();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<HttpInterceptorService>();
builder.Services.AddScoped<RefreshTokenService>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

var host = builder.Build();

await host.SetDefaultCulture();

await host.RunAsync();
