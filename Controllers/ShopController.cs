﻿using AutoMapper;
using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ShopController(IProductService productService, IMapper mapper, ICategoryService categoryService, IBrandService brandService, IColorService colorService, UserManager<AppUser> userManager, ICommentService commentService, IImageService imageService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
            _brandService = brandService;
            _colorService = colorService;
            _userManager = userManager;
            _commentService = commentService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _productService.GetAll();
            List<GetProductVM> productsVM = _mapper.Map<List<GetProductVM>>(products);
            List<Brand> brands = await _brandService.GetAll();
            List<Category> categories = await _categoryService.GetAll();
            List<Color> colors = await _colorService.GetAll();

            ShopVM shopVM = new()
            {
                GetProductVMs = productsVM,
                Brands = brands,
                Categories = categories,
                Colors = colors
            };

            return View(model: shopVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _productService.Get(id);
            GetProductDetailVM productDetailVM = _mapper.Map<GetProductDetailVM>(product);

            List<Comment> comments = new();
            if (productDetailVM.Comments is not null)
            {
                foreach (var comment in productDetailVM.Comments)
                {
                    comment.AppUser = await _userManager.FindByIdAsync(comment.AppUserId);
                    if (comment.AppUser.ImageId is not null)
                    {
                        comment.AppUser.Image = await _imageService.Get(comment.AppUser.ImageId);
                    }
                    comments.Add(comment);
                }
            }

            return View(model: productDetailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(GetProductDetailVM vm, int? id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction(nameof(Detail), vm);
            //}

            var comment = vm.Comment;
            comment.AppUser = await _userManager.GetUserAsync(User);
            var product = await _productService.Get(id);
            comment.Product = product;

            await _commentService.Create(comment);
            await _commentService.SaveChanges();

            return RedirectToAction(nameof(Detail), vm);
        }
    }
}
