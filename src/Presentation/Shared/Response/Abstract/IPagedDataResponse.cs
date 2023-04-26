namespace BlazorEcommerce.Shared.Response.Abstract;

public interface IPagedDataResponse<T> : IResponse
{
    int TotalItems { get; }
    T Data { get; }
}
