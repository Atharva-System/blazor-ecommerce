namespace BlazorEcommerce.Shared.Response.Abstract;

public interface IErrorResponse : IResponse
{
    List<string> Messages { get; }
}
