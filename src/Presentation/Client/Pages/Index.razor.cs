using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorEcommerce.Client.Pages;

public partial class Index : IDisposable
{
    private ErrorBoundary? errorBoundary;

    [Parameter]
    public string CategoryUrl { get; set; } = null;

    [Parameter]
    public string SearchText { get; set; } = null;

    [Parameter]
    public int Page { get; set; } = 1;

    [Inject]
    public HttpInterceptorService Interceptor { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Interceptor.RegisterEvent();

        errorBoundary?.Recover();

        if (SearchText != null)
        {
            await ProductService.SearchProducts(SearchText, Page);
        }
        else
        {
            await ProductService.GetProducts(CategoryUrl);
        }

    }

    private void ResetError()
    {
        errorBoundary?.Recover();
    }

    public void Dispose()
    {
        Interceptor.DisposeEvent();
    }
}
