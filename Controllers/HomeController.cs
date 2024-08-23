using AutoMapper;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public HomeController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _productService.GetAll();

        List<Product> sliderProducts = products.OrderByDescending(n => n.CreateDate).ToList();
        List<Product> discountedProducts = products.OrderByDescending(n => n.CreateDate).Where(n => n.DiscountValue != 0).Take(12).ToList();

        HomeVM homeVM = new()
        {
            SliderProducts = _mapper.Map<List<GetProductVM>>(sliderProducts),
            BannerProducts = _mapper.Map<List<GetProductVM>>(sliderProducts),
            DiscountedProducts = _mapper.Map<List<GetProductVM>>(discountedProducts),
            BrandsProducts = _mapper.Map<List<GetProductVM>>(sliderProducts)
        };


        return View(model: homeVM);
    }

}
