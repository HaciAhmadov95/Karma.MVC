﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "Admin,SuperAdmin")]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
