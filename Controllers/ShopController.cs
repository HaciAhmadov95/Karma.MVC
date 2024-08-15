using Karma.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;

        public ShopController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
