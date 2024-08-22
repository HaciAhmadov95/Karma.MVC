using Karma.MVC.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly SettingRepository _settingRepository;

        public ContactController(SettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public IActionResult Index()
        {
            ViewData["settings"] = _settingRepository.GetAll();

            return View();
        }

        public IActionResult SendMessageToAdmin(string message)
        {



            return View();
        }
    }
}
