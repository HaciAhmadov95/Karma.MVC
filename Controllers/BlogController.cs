using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(IBlogService blogService, IBlogCategoryService blogCategoryService, UserManager<AppUser> userManager, ICommentService commentService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _userManager = userManager;
            _commentService = commentService;
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

        public async Task<IActionResult> Detail(int id)
        {
            List<Blog> blogs = await _blogService.GetAll();
            Blog blog = await _blogService.Get(id);
            List<BlogCategory> blogCategories = await _blogCategoryService.GetAll();
            List<Blog> popularBlogs = blogs.OrderByDescending(n => n.Comments.Count).Take(4).ToList();

            BlogDetailVM blogDetailVM = new()
            {
                Blog = blog,
                BlogCategories = blogCategories,
                PopularBlogs = popularBlogs
            };

            return View(model: blogDetailVM);
        }

        public async Task<IActionResult> AddComment(int? id, string content)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction(nameof(Detail), vm);
            //}
            Comment comment = new()
            {
                Content = content,
                AppUser = await _userManager.GetUserAsync(User),
                Blog = await _blogService.Get(id),
            };

            await _commentService.Create(comment);
            await _commentService.SaveChanges();

            return RedirectToAction(nameof(Detail), comment);
        }
    }
}
