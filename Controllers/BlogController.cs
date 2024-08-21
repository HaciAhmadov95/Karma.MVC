using Karma.MVC.Models;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _blogService.GetAll();
            List<BlogCategory> blogCategories = await _blogCategoryService.GetAll();
            List<Blog> popularBlogs = blogs.OrderByDescending(n => n.Comments.Count).Take(4).ToList();


            BlogVM blogVM = new()
            {
                Blogs = blogs,
                BlogCategories = blogCategories,
                PopularBlogs = popularBlogs
            };

            return View(model: blogVM);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
