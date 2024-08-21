using Karma.MVC.Models.Identity;
using Karma.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "SuperAdmin")]
public class SubscriberController : Controller
{
    private readonly ISubscriberService _subscriberService;
    private readonly UserManager<AppUser> _userManager;

    public SubscriberController(ISubscriberService subscriberService, UserManager<AppUser> userManager)
    {
        _subscriberService = subscriberService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var subscribers = await _subscriberService.GetAll();
        return View(model: subscribers);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        await _subscriberService.Delete(id);
        await _subscriberService.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult SendEmail()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendEmail(string message)
    {
        if (!ModelState.IsValid)
        {
            return View(model: message);
        }

        var subscribers = await _subscriberService.GetAll();

        var user = await _userManager.GetUserAsync(User);
        string from = "ahmadovhaciaga@gmail.com";
        string subject = $"Karma Features";
        string body = $"{message}";

        foreach (var subscriber in subscribers)
        {
            string to = subscriber.SubscriberEmail;
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
            client.Credentials = new NetworkCredential(from, "zjdyemefrbjoeyie");
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        return RedirectToAction(nameof(Index));
    }
}