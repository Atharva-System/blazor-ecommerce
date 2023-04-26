using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;
using System.Text.Json.Serialization;

namespace BlazorEcommerce.Shared.Response.Concrete;

public class DataResponse<T> : IDataResponse<T>
{
    public bool Success { get; } = true;
    public T Data { get; set; }

    public int StatusCode { get; }
    public string Message { get; set; }

    [JsonConstructor]
    public DataResponse(T data, int statuscode)
    {
        Data = data;
        StatusCode = statuscode;
        Message = Messages.DataFound;
    }

    public DataResponse(T data, int statuscode, string message)
    {
        Data = data;
        StatusCode = statuscode;
        Message = message;
    }
}