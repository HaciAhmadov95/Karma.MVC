using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IProductService _productService;
		private readonly UserManager<AppUser> _userManager;

		public CartController(ICartService cartService, UserManager<AppUser> userManager, IProductService productService)
		{
			_cartService = cartService;
			_userManager = userManager;
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Authentication");
			}

			var user = await _userManager.GetUserAsync(User);
			var cart = await _cartService.Get(user.CartId);

			return View(model: cart);
		}

		public async Task<IActionResult> AddToCart(int? id)
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
			var cart = await _cartService.Get(user.CartId);

			products.AddRange(cart.Products);
			products.Add(await _productService.Get(id));
			cart.Products = products;
			await _cartService.Update(cart.Id, cart);
			await _cartService.SaveChanges();


			return View("Index", cart);
		}

		public async Task<IActionResult> DeleteFromCart(int? id)
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

			var cart = await _cartService.Get(user.CartId);

			List<Product> products = new();

			foreach (var product in cart.Products)
			{
				if (product.Id != id)
				{
					products.Add(product);
				}
			}

			cart.Products = products;

			await _cartService.Update(cart.Id, cart);
			await _cartService.SaveChanges();

			return View("Index", cart);
		}
	}
}
