using Karma.MVC.Helpers.Extensions;
using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Karma.MVC.Controllers;

public class AccountController : Controller
{
	private readonly UserManager<AppUser> _userManager;
	private readonly IImageService _imageService;
	private readonly IWebHostEnvironment _env;

	public AccountController(UserManager<AppUser> userManager, IImageService imageService, IWebHostEnvironment env)
	{
		_userManager = userManager;
		_imageService = imageService;
		_env = env;
	}

	public async Task<IActionResult> Index()
	{
		AppUser applicationUser = await _userManager.GetUserAsync(User);

		if (applicationUser.ImageId is not null)
		{
			applicationUser.Image = await _imageService.Get(applicationUser.ImageId);
		}

		return View(applicationUser);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> UpdateProfile(AppUser appUser)
	{
		//if (!ModelState.IsValid)
		//{
		//	return View(nameof(Index), appUser);
		//}

		AppUser applicationUser = await _userManager.GetUserAsync(User);
		applicationUser.Image = await _imageService.Get(applicationUser.ImageId);

		if (appUser.ProfileImage != null)
		{
			string fileName = await appUser.ProfileImage.CreateFile(_env);

			Image image = new()
			{
				Url = fileName,
				IsMain = true,
				AppUser = applicationUser
			};

			if (appUser.ImageId is not null)
			{
				await _imageService.Update((int)appUser.ImageId, image);
				await _imageService.SaveChanges();

			}
			else
			{
				await _imageService.Create(image);
				await _imageService.SaveChanges();
			}

			applicationUser.Image = image;
		}

		applicationUser.Firstname = appUser.Firstname;
		applicationUser.Lastname = appUser.Lastname;

		await _userManager.UpdateAsync(applicationUser);

		return View(nameof(Index), applicationUser);

	}
}
