using BlazorEcommerce.Client.CustomExceptions;
using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace BlazorEcommerce.Client.Services;

public class HttpInterceptorService
{
    private readonly HttpClientInterceptor _interceptor;
    private readonly NavigationManager _navManager;

    public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
    {
        _interceptor = interceptor;
        _navManager = navManager;
    }

    public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;

    private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
    {
        string message = string.Empty;

        if (!e.Response.IsSuccessStatusCode)
        {
            var statusCode = e.Response.StatusCode;

            switch (statusCode)
            {
                case HttpStatusCode.NotFound:
                    _navManager.NavigateTo("/404");
                    message = "The requested resorce was not found.";
                    break;
                case HttpStatusCode.Unauthorized:
                    _navManager.NavigateTo("/unauthorized");
                    message = "User is not authorized";
                    break;
                default:
                    _navManager.NavigateTo("/500");
                    message = "Something went wrong, please contact Administrator";
                    break;
            }

            throw new HttpResponseException(message);
        }
    }

    public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
}