using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class ColorController : Controller
{
	private readonly IColorService _colorService;

	public ColorController(IColorService colorService)
	{
		_colorService = colorService;
	}

	public async Task<IActionResult> Index()
	{
		List<Color> data;

		try
		{
			data = await _colorService.GetAll();
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
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(Color entity)
	{
		if (!ModelState.IsValid)
		{
			return View(entity);
		}

		await _colorService.Create(entity);
		await _colorService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var data = await _colorService.Get(id);

		return View(data);
	}

	public async Task<IActionResult> Update(int id, Color color)
	{
		if (!ModelState.IsValid)
		{
			return View(model: color);
		}

		await _colorService.Update(id, color);
		await _colorService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}

	public async Task<IActionResult> Delete(int? id)
	{
		await _colorService.Delete(id);
		await _colorService.SaveChanges();

		return RedirectToAction(nameof(Index));
	}
}
