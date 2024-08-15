using Karma.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
