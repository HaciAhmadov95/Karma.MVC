using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;
        private readonly IProductService _productService;
        private readonly UserManager<AppUser> _userManager;

        public WishlistController(UserManager<AppUser> userManager, IProductService productService, IWishlistService wishlistService)
        {
            _userManager = userManager;
            _productService = productService;
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var user = await _userManager.GetUserAsync(User);
            var wishlist = await _wishlistService.Get(user.WishlistId);

            return View(model: wishlist);
        }

        public async Task<IActionResult> AddToWishlist(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            List<Product> products = new();

            var user = await _userManager.GetUserAsync(User);
            var wishlist = await _wishlistService.Get(user.WishlistId);

            foreach (var product in wishlist.Products)
            {
                if (product.Id == id)
                {
                    return RedirectToAction("Index", wishlist);
                }
            }

            products.AddRange(wishlist.Products);
            products.Add(await _productService.Get(id));
            wishlist.Products = products;
            await _wishlistService.Update(wishlist.Id, wishlist);
            await _wishlistService.SaveChanges();

            return RedirectToAction("Index", wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromWishlist(int? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var user = await _userManager.GetUserAsync(User);

            var wishlist = await _wishlistService.Get(user.WishlistId);

            List<Product> products = new();

            foreach (var product in wishlist.Products)
            {
                if (product.Id != id)
                {
                    products.Add(product);
                }
            }

            wishlist.Products = products;

            await _wishlistService.Update(wishlist.Id, wishlist);
            await _wishlistService.SaveChanges();

            return RedirectToAction("Index", wishlist);
        }
    }
}
