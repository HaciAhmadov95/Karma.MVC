using Karma.MVC.Models.Base;
using Karma.MVC.Models.Identity;

namespace Karma.MVC.Models;

public class Cart : BaseEntity
{
    public double TotalPrice { get; set; }
    public AppUser? AppUser { get; set; }
    public ICollection<Product>? Products { get; set; }
}
