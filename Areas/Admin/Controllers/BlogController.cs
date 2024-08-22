using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class BlogController : Controller
{
	private readonly IBlogService _blogService;
	private readonly IBlogCategoryService _blogCategoryService;
	private readonly UserManager<AppUser> _userManager;

	public BlogController(IBlogService blogService,
						  IBlogCategoryService blogCategoryService,
						  UserManager<AppUser> userManager)
	{
		_blogService = blogService;
		_blogCategoryService = blogCategoryService;
		_userManager = userManager;
	}

	public async Task<IActionResult> Index()
	{
		var blogs = await _blogService.GetAll();

		return View(model: blogs);
	}

	public async Task<IActionResult> Detail(int? id)
	{
		Blog data;

		try
		{
			data = await _blogService.Get(id);
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
		ViewData["blogCategories"] = await _blogCategoryService.GetAll();

		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Blog entity)
	{

		if (entity.ImageFile is null)
		{
			ModelState.AddModelError("ImageFile", "Image can not be empty");
			return View(entity);
		}

		AppUser applicationUser = await _userManager.GetUserAsync(User);
		entity.AppUser = applicationUser;

		await _blogService.Create(entity);
		await _blogService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var data = await _blogService.Get(id);

		return View(model: data);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Update(int id, Blog entity)
	{
		if (!ModelState.IsValid)
		{
			return View(entity);
		}

		await _blogService.Update(id, entity);
		await _blogService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(int? id)
	{
		await _blogService.Delete(id);
		await _blogService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public IActionResult CreateCategory()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategory(BlogCategory blogCategory)
	{
		await _blogCategoryService.Create(blogCategory);
		await _blogCategoryService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}
}
