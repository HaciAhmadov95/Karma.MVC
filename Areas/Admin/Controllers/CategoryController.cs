using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        List<Category> data;

        try
        {
            data = await _categoryService.GetAll();
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
    public async Task<IActionResult> Create(Category entity)
    {
        if (!ModelState.IsValid)
        {
            return View(entity);
        }

        await _categoryService.Create(entity);
        await _categoryService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var data = await _categoryService.Get(id);

        return View(data);
    }

    public async Task<IActionResult> Update(int id, Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        await _categoryService.Update(id, category);
        await _categoryService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        await _categoryService.Delete(id);
        await _categoryService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
