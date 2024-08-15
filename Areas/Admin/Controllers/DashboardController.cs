using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class DashboardController : Controller
{
    private readonly IImageService _imageService;
    private readonly ICommentService _commentService;
    private readonly UserManager<AppUser> _userManager;

    public DashboardController(IImageService imageService, ICommentService commentService, UserManager<AppUser> userManager)
    {
        _imageService = imageService;
        _commentService = commentService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        List<Comment> comments = await _commentService.GetAll();

        DashboardVM dashboardVm = new()
        {
            Comments = comments
        };

        return View(model: dashboardVm);
    }

    public async Task<IActionResult> DeleteComment(int? id)
    {
        var comment = await _commentService.Get(id);

        comment.IsDeleted = true;
        await _commentService.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
