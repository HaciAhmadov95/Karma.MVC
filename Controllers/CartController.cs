using Karma.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
