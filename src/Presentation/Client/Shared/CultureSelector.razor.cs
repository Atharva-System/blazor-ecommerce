using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;

namespace BlazorEcommerce.Client.Shared
{
    public partial class CultureSelector
	{
		[Inject]
		public NavigationManager NavManager { get; set; }

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		CultureInfo[] cultures = new[]
		{
			new CultureInfo("en-US"),
			new CultureInfo("de-DE")
		};

		CultureInfo Culture
		{
			get => CultureInfo.CurrentCulture;
			set
			{
				if (CultureInfo.CurrentCulture != value)
				{
					var js = (IJSInProcessRuntime)JSRuntime;
					js.InvokeVoid("blazorCulture.set", value.Name);

					NavManager.NavigateTo(NavManager.Uri, forceLoad: true);
				}
			}
		}
	}
}
