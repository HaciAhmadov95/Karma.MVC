using Karma.MVC.Models;
using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using static Karma.MVC.Utilities.Helpers.Enums;

namespace Karma.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ISubscriberService _subscriberService;

        public AuthenticationController(UserManager<AppUser> userManager,
               RoleManager<IdentityRole> roleManager,
               SignInManager<AppUser> signInManager,
               ICartService cartService,
               IProductService productService,
               ISubscriberService subscriberService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _cartService = cartService;
            _productService = productService;
            _subscriberService = subscriberService;
        }

        [HttpGet(nameof(Register))]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json("Ok");
            }

            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            Cart cart = new()
            {
                TotalPrice = 0
            };

            Wishlist wishlist = new();

            AppUser appUser = new AppUser()
            {
                Firstname = registerVM.Firstname,
                Lastname = registerVM.Lastname,
                Email = registerVM.Email,
                UserName = registerVM.Username,
                Cart = cart,
                Wishlist = wishlist
            };

            var result = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                };
                return View(registerVM);
            };

            var roleResult = await _userManager.AddToRoleAsync(appUser, Roles.Member.ToString());

            if (!roleResult.Succeeded)
            {
                foreach (var item in roleResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                };
                return View(registerVM);
            };

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, username = appUser.UserName }, HttpContext.Request.Scheme);

            SendEmail(appUser.Email, confirmationLink);

            return RedirectToAction("Index", controllerName: "Home");

        }

        [HttpGet(nameof(Login))]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json("Ok");
            }

            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            AppUser appUser = await _userManager.FindByNameAsync(loginVM.Username);

            if (appUser is null)
            {
                ModelState.AddModelError("", "Username not found!");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, true);

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Please confirm your account!");
                return View(loginVM);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your profile has been locked!\nPlease try later!");
                return View(loginVM);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username not found!");
                return View(loginVM);
            }

            if (await _userManager.IsInRoleAsync(appUser, Roles.Admin.ToString()))
            {
                return RedirectToAction("Index", controllerName: "Dashboard", new { area = "Admin" });
            }


            return RedirectToAction("Index", controllerName: "Home");
        }

        public async Task<ActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", controllerName: "Home");
        }

        public IActionResult SendEmail(string userEmail, string confirmationLink)
        {
            string from = "ahmadovhaciaga@gmail.com";
            string to = userEmail;
            string subject = "Confirm you email";
            string body = $"<a href=\"{confirmationLink}\">Click here for confirm your account!</a>";

            MailMessage mailMessage = new(
                from,
                to,
                subject,
                body
            );
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;

            SmtpClient client = new("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(from, "vktvykutjpgmwvyz");
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }

            return Json("Ok");
        }

        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(item.ToString()));
                }
            }
        }

        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            AppUser appUser = await _userManager.FindByNameAsync(username);

            if (appUser is null)
            {
                return Json("User could not Found");
            }

            var result = await _userManager.ConfirmEmailAsync(appUser, token);

            if (!result.Succeeded)
            {
                return Json("Your token is invalid");
            }

            return RedirectToAction("Index", controllerName: "Home");
        }
    }
}
