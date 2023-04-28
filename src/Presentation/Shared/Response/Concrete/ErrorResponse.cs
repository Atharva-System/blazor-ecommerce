using BlazorEcommerce.Shared.Response.Abstract;

namespace BlazorEcommerce.Shared.Response.Concrete;

public class ErrorResponse : IErrorResponse
{
    public bool Success { get; } = false;
    public int StatusCode { get; }
    public List<string> Messages { get; private set; } = new List<string>();

    public ErrorResponse(int statuscode, List<string> errors)
    {
        StatusCode = statuscode;
        Messages = errors;
    }

    public ErrorResponse(int statuscode, string error)
    {
        StatusCode = statuscode;
        Messages.Add(error);
    }
}
