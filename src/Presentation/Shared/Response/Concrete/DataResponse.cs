using BlazorEcommerce.Shared.Response.Abstract;
using System.Text.Json.Serialization;

namespace BlazorEcommerce.Shared.Response.Concrete;

public class DataResponse<T> : IDataResponse<T>
{
    public bool Success { get; } = true;
    public T Data { get; set; }

    public int StatusCode { get; }
    public List<string> Messages { get; private set; } = new List<string>();

    [JsonConstructor]
    public DataResponse(T data, int statuscode)
    {
        Data = data;
        StatusCode = statuscode;
        Messages.Add(Constant.Messages.NotFound);
    }

    public DataResponse(T data, int statuscode, string message)
    {
        Data = data;
        StatusCode = statuscode;
        Messages.Add(message);
    }

    public DataResponse(T data, int statuscode, List<string> messages)
    {
        Data = data;
        StatusCode = statuscode;
        Messages = messages;
    }
}