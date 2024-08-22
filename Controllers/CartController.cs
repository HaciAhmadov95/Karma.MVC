using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		private readonly IProductService _productService;
		private readonly ICartProductService _cartProductService;
		private readonly UserManager<AppUser> _userManager;

		public CartController(ICartService cartService, UserManager<AppUser> userManager, IProductService productService, ICartProductService cartProductService)
		{
			_cartService = cartService;
			_userManager = userManager;
			_productService = productService;
			_cartProductService = cartProductService;
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

			var user = await _userManager.GetUserAsync(User);
			var cart = await _cartService.Get(user.CartId);

			CartProduct cartProduct = new()
			{
				Cart = cart,
				Product = await _productService.Get(id),
				Quantity = 1
			};

			await _cartProductService.Create(cartProduct);
			await _cartProductService.SaveChanges();

			return RedirectToAction("Index", cart);
		}

		public async Task<IActionResult> DeleteFromCart(int id)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Authentication");
			}

			var user = await _userManager.GetUserAsync(User);
			var cart = await _cartService.Get(user.CartId);

			await _cartProductService.DeleteProduct(cart.Id, id);
			await _cartService.SaveChanges();

			return RedirectToAction("Index", "Cart", cart);
		}

		public async Task<IActionResult> ChangeQuantity(int quantity, int productId)
		{
			var user = await _userManager.GetUserAsync(User);
			var cart = await _cartService.Get(user.CartId);

			List<CartProductVM> cartProductVMs = await _cartService.ChangeQuantity(quantity, productId, cart.Id);

			return Json(cartProductVMs);
		}
	}
}
