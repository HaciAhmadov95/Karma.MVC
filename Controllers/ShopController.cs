using AutoMapper;
using Karma.MVC.Models;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ShopController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productService.GetAll();
            List<GetProductVM> productsVM = _mapper.Map<List<GetProductVM>>(products);

            ShopVM shopVM = new()
            {
                GetProductVMs = productsVM
            };

            return View(model: shopVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _productService.Get(id);
            GetProductDetailVM productDetailVM = _mapper.Map<GetProductDetailVM>(product);

            return View(model: productDetailVM);
        }
    }
}
