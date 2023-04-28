namespace BlazorEcommerce.Shared.Response.Abstract;

public interface IDataResponse<T> : IResponse
{
    T Data { get; }
    List<string> Messages { get; }
}
