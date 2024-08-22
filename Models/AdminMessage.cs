using Karma.MVC.Models.Base;
using Karma.MVC.Models.Identity;

namespace Karma.MVC.Models;

public class AdminMessage : BaseEntity
{
	public string Message { get; set; }
	public string AppUserId { get; set; }
	public AppUser AppUser { get; set; }
}
