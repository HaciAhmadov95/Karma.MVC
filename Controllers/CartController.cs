using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
