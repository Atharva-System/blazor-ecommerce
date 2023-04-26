using Microsoft.AspNetCore.Http;

namespace BlazorEcommerce.Shared
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            if (Success)
            {
                StatusCode = StatusCodes.Status200OK;
            }
            else
            {
                StatusCode = StatusCodes.Status404NotFound;
            }
        }

        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public List<string> ErrorMessages = new();
        public int StatusCode { get; set; }
    }
}
