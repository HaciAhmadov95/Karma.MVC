using AutoMapper;
using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
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
    private readonly IColorService _colorService;
    private readonly IWebHostEnvironment _env;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IImageService imageService, UserManager<AppUser> userManager, IWebHostEnvironment env, IColorService colorService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _brandService = brandService;
        _imageService = imageService;
        _colorService = colorService;
        _userManager = userManager;
        _env = env;
        _mapper = mapper;
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
        ViewData["Colors"] = await _colorService.GetAll();

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateProductVM entity)
    {
        ViewData["Categories"] = await _categoryService.GetAll();
        ViewData["Brands"] = await _brandService.GetAll();
        ViewData["Colors"] = await _colorService.GetAll();

        if (entity.ImageFile is null)
        {
            ModelState.AddModelError("ImageFile", "Image can not be empty");
            return View(entity);
        }

        Product product = _mapper.Map<Product>(entity);

        AppUser applicationUser = await _userManager.GetUserAsync(User);
        product.AppUser = applicationUser;

        List<Color> colors = new();
        foreach (var color in entity.Colors)
        {
            if (color.IsSelected == true)
            {
                Color colorData = await _colorService.Get(color.Id);
                color.IsDeleted = false;
                colors.Add(colorData);
            }
        }

        product.Colors = colors;

        await _productService.Create(product);
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
        await _productService.SaveChanges();


        return RedirectToAction(nameof(Index));
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