global using BlazorEcommerce.Shared;
global using BlazorEcommerce.Shared.Response.Abstract;
using BlazorEcommerce.Application;
using BlazorEcommerce.Application.Contracts.Identity;
using BlazorEcommerce.Identity;
using BlazorEcommerce.Identity.Contexts;
using BlazorEcommerce.Infrastructure;
using BlazorEcommerce.Persistence;
using BlazorEcommerce.Persistence.Contexts;
using BlazorEcommerce.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Initialise and seed the database
using (var scope = app.Services.CreateScope())
{
    try
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();

        var persistenceInitialiser = scope.ServiceProvider.GetRequiredService<PersistenceDbContextInitialiser>();
        await persistenceInitialiser.InitialiseAsync();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialisation.");

        throw;
    }
}

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
