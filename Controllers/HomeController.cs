using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers;

public class HomeController : Controller
{

    public async Task<IActionResult> Index()
    {
        return View();
    }

}
