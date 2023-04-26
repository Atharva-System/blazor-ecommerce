global using BlazorEcommerce.Server.Data;
global using BlazorEcommerce.Server.Services.CartService;
global using BlazorEcommerce.Server.Services.OrderService;
global using BlazorEcommerce.Server.Services.PaymentService;
global using BlazorEcommerce.Server.Services.ProductService;
global using BlazorEcommerce.Server.Services.ProductTypeService;
global using BlazorEcommerce.Shared;
global using BlazorEcommerce.Shared.Response.Abstract;
global using Microsoft.EntityFrameworkCore;
using BlazorEcommerce.Application;
using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Identity;
using BlazorEcommerce.Infrastructure;
using BlazorEcommerce.Persistence;
using BlazorEcommerce.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});  

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//builder.Services.AddOpenApiDocument(configure =>
//{
//    configure.Title = "CleanArchitecture API";
//    //configure.AddSecurity("JWT", Enumerable.Empty<string>(),
//    //    new OpenApiSecurityScheme
//    //    {
//    //        Type = OpenApiSecuritySchemeType.ApiKey,
//    //        Name = "Authorization",
//    //        In = OpenApiSecurityApiKeyLocation.Header,
//    //        Description = "Type into the textbox: Bearer {your JWT token}."
//    //    });

//    //configure.OperationProcessors.Add(
//    //    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
//});

builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistanceServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IProductTypeService, ProductTypeService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseSwaggerUI();
//app.UseOpenApi();
//app.UseSwaggerUi3(configure =>
//{
//    configure.DocumentPath = "/api/v1/openapi.json";
//});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
