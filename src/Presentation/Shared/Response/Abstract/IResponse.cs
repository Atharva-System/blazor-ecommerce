namespace BlazorEcommerce.Shared.Response.Abstract;

public interface IResponse
{
    bool Success { get; }
    int StatusCode { get; }
}
