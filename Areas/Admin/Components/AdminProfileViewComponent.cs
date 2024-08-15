using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Components;

public class AdminProfileViewComponent : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IImageService _imageService;

    public AdminProfileViewComponent(UserManager<AppUser> userManager, IImageService imageService)
    {
        _userManager = userManager;
        _imageService = imageService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        if (user.ImageId is not null)
        {
            user.Image = await _imageService.Get(user.ImageId);
        }

        return View(user);
    }
}
