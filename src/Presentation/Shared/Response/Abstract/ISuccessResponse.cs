namespace BlazorEcommerce.Shared.Response.Abstract;

public interface ISuccessResponse : IResponse
{
    string Message { get; }
}
