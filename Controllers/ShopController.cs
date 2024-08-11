using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
