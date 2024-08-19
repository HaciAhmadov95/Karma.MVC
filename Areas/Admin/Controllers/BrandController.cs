using Karma.MVC.Models;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class BrandController : Controller
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    public async Task<IActionResult> Index()
    {
        List<Brand> data;

        try
        {
            data = await _brandService.GetAll();
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
    public async Task<IActionResult> Create(Brand entity)
    {
        if (!ModelState.IsValid)
        {
            return View(entity);
        }

        await _brandService.Create(entity);
        await _brandService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var data = await _brandService.Get(id);

        return View(data);
    }

    public async Task<IActionResult> Update(int id, Brand brand)
    {
        if (!ModelState.IsValid)
        {
            return View(model: brand);
        }

        await _brandService.Update(id, brand);
        await _brandService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        await _brandService.Delete(id);
        await _brandService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
