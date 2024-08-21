using Karma.MVC.Models.Identity;
using Karma.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Karma.MVC.Areas.Admin.Controllers;

[Area("admin"), Authorize(Roles = "SuperAdmin")]
public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var allUser = await _userManager.Users.ToListAsync();
        List<GetUserVM> userVms = new();

        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var user in allUser)
        {
            GetUserVM vm = new()
            {
                User = user,
                UserRole = await _userManager.GetRolesAsync(user),
                AllRoles = roles
            };
            userVms.Add(vm);
        }

        return View(model: userVms);
    }

    public async Task<IActionResult> ChangeRole(string roleName, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            await _userManager.RemoveFromRoleAsync(user, userRole.ToString());
        }
        await _userManager.AddToRoleAsync(user, roleName.ToString());

        var allUser = await _userManager.Users.ToListAsync();
        List<GetUserVM> userVms = new();

        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var user1 in allUser)
        {
            GetUserVM vm = new()
            {
                User = user1,
                UserRole = await _userManager.GetRolesAsync(user1),
                AllRoles = roles
            };
            userVms.Add(vm);
        }

        return View(nameof(Index), userVms);
    }

    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        await _userManager.DeleteAsync(user);

        var allUser = await _userManager.Users.ToListAsync();
        List<GetUserVM> userVms = new();

        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var user1 in allUser)
        {
            GetUserVM vm = new()
            {
                User = user1,
                UserRole = await _userManager.GetRolesAsync(user1),
                AllRoles = roles
            };
            userVms.Add(vm);
        }

        return View(nameof(Index), userVms);
    }
}
