using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
