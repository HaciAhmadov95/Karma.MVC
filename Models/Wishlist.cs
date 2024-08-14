using Karma.MVC.Models.Base;
using Karma.MVC.Models.Identity;

namespace Karma.MVC.Models;

public class Wishlist : BaseEntity
{
    public AppUser AppUser { get; set; }
    public ICollection<Product> Products { get; set; }
}
