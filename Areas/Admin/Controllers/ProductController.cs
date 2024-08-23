using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class ProductController : Controller
{
	private readonly IProductService _productService;
	private readonly ICategoryService _categoryService;
	private readonly IBrandService _brandService;
	private readonly IImageService _imageService;
	private readonly IWebHostEnvironment _env;
	private readonly UserManager<AppUser> _userManager;

	public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IImageService imageService, UserManager<AppUser> userManager, IWebHostEnvironment env)
	{
		_productService = productService;
		_categoryService = categoryService;
		_brandService = brandService;
		_imageService = imageService;
		_userManager = userManager;
		_env = env;
	}

	public async Task<IActionResult> Index()
	{
		List<Product> data;

		try
		{
			data = await _productService.GetAll();
		}
		catch (ArgumentNullException ex)
		{
			throw ex;
		}
		catch (NullReferenceException ex)
		{
			throw ex;
		}
		catch (Exception)
		{
			throw;
		}

		return View(model: data);
	}

	public async Task<IActionResult> Detail(int? id)
	{
		Product data;

		try
		{
			data = await _productService.Get(id);
		}
		catch (ArgumentNullException ex)
		{
			throw ex;
		}
		catch (NullReferenceException ex)
		{
			throw ex;
		}
		catch (Exception)
		{
			throw;
		}

		return View(model: data);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		ViewData["Categories"] = await _categoryService.GetAll();
		ViewData["Brands"] = await _brandService.GetAll();

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Product entity)
	{
		ViewData["Categories"] = await _categoryService.GetAll();

		if (entity.ImageFile is null)
		{
			ModelState.AddModelError("ImageFile", "Image can not be empty");
			return View(entity);
		}

		AppUser applicationUser = await _userManager.GetUserAsync(User);
		entity.AppUser = applicationUser;

		await _productService.Create(entity);
		await _productService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var data = await _productService.Get(id);

		return View(model: data);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(int id, Product entity)
	{
		if (!ModelState.IsValid)
		{
			return View(entity);
		}



		await _productService.Update(id, entity);
		await _productService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(int? id)
	{
		await _productService.Delete(id);

		return View(nameof(Index));
	}

	public async Task<IActionResult> DeleteImage(int? id)
	{
		await _imageService.Delete(id);
		await _imageService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> MakeDiscount(int id)
	{
		var data = await _productService.Get(id);

		return View(model: data);
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> MakeDiscount(int id, double discountValue)
	{
		var data = await _productService.Get(id);

		data.DiscountValue = discountValue;

		await _productService.Update(id, data);
		await _productService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}
}