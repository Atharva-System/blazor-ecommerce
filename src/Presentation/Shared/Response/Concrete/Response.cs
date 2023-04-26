using BlazorEcommerce.Shared.Response.Abstract;

namespace BlazorEcommerce.Shared.Response.Concrete;

public class Response : IResponse
{
    public bool Success { get; }

    public int StatusCode { get; }

    public Response(bool success, int statuscode)
    {
        Success = success;
        StatusCode = statuscode;
    }

    public Response(bool success)
    {
        Success = success;
    }
}
