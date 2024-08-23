using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
	public class ContactController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}
	}
}
