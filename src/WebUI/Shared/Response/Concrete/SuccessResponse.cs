using BlazorEcommerce.Shared.Constant;
using BlazorEcommerce.Shared.Response.Abstract;

namespace BlazorEcommerce.Shared.Response.Concrete;

public class SuccessResponse : ISuccessResponse
{
    public bool Success { get; } = true;
    public string Message { get; }
    public int StatusCode { get; }

    private bool IsAdd { get; set; }

    public SuccessResponse(string message = "")
    {
        StatusCode = HttpStatusCodes.Accepted;
        Message = !string.IsNullOrEmpty(message) ? message : Messages.Success;
    }

    public SuccessResponse(int statuscode, bool isAdd)
    {
        IsAdd = isAdd;
        StatusCode = statuscode;

        if (IsAdd)
        {
            Message = Messages.AddedSuccesfully;
        }
        else
        {
            Message = Messages.UpdatedSuccessfully;
        }
    }
}
