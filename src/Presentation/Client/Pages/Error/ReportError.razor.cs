using Microsoft.AspNetCore.Components;

namespace BlazorEcommerce.Client.Pages.Error
{
    public partial class ReportError
	{
		[Parameter]
		public int ErrorCode { get; set; }

		[Parameter]
		public string ErrorDescription { get; set; }
	}
}
