using Karma.MVC.Helpers.Extensions;
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

        List<Image> images = new();

        foreach (var imageFile in entity.ImageFile)
        {
            string fileName = await imageFile.CreateFile(_env);

            Image image = new();
            image.Url = fileName;
            image.IsMain = false;
            images.Add(image);
        }

        Image mainImage = new();
        string mainFileName = await entity.MainImage.CreateFile(_env);
        mainImage.Url = mainFileName;
        mainImage.IsMain = true;
        images.Add(mainImage);

        entity.Images = images;

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

        List<Image> currentImages = new();
        var data = await _productService.Get(id);

        if (entity.ImageFile is not null)
        {
            for (int i = 0; i < data.Images.Where(n => n.IsMain == false).ToList().Count; i++)
            {
                currentImages.Add(data.Images.Where(n => n.IsMain == false).ToList()[i]);
            }

            foreach (var imageFile in entity.ImageFile)
            {
                string fileName = await imageFile.CreateFile(_env);

                Image image = new();
                image.Url = fileName;
                image.IsMain = false;
                currentImages.Add(image);
            }

            var images = data.Images;
            currentImages.AddRange(images);
        }
        else
        {
            for (int i = 0; i < data.Images.Where(n => n.IsMain == false).ToList().Count; i++)
            {
                currentImages.Add(data.Images.Where(n => n.IsMain == false).ToList()[i]);
            }
        }

        if (entity.MainImage is not null)
        {
            string fileName = await entity.MainImage.CreateFile(_env);

            Image image = new();
            image.Url = fileName;
            image.IsMain = true;
            currentImages.Add(image);

            await _imageService.Delete(data.Images.Where(n => n.IsMain == true).FirstOrDefault().Id);
        }
        else
        {
            currentImages.Add(data.Images.Where(n => n.IsMain == true).FirstOrDefault());
        }

        entity.Images = currentImages;

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
}