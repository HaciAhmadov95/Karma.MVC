using Karma.MVC.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Karma.MVC.ViewModels;

public class GetUserVM
{
	public AppUser User { get; set; }
	public IList<string> UserRole { get; set; }
	public List<IdentityRole> AllRoles { get; set; }
}
