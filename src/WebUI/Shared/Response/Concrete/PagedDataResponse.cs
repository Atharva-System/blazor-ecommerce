using BlazorEcommerce.Shared.Response.Abstract;

namespace BlazorEcommerce.Shared.Response.Concrete;

public class PagedDataResponse<T> : IPagedDataResponse<T>
{
    public bool Success { get; } = true;
    public int TotalItems { get; }

    public T Data { get; }

    public int StatusCode { get; }

    public PagedDataResponse(T data, int statuscode, int totalitems)
    {
        Data = data;
        StatusCode = statuscode;
        TotalItems = totalitems;
    }
}
