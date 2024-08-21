using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Components;

public class HeaderViewComponent : ViewComponent
{
	public async Task<IViewComponentResult> InvokeAsync()
	{
		return View();
	}
}
