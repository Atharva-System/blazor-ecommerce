namespace BlazorEcommerce.Shared.Response.Concrete;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }

    public int StatusCode { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
}