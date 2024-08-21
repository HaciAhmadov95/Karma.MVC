using Karma.MVC.Models;

namespace Karma.MVC.ViewModels;

public class ShopVM
{
    public List<GetProductVM> GetProductVMs { get; set; }
    public List<Category> Categories { get; set; }
    public List<Brand> Brands { get; set; }
    public List<Color> Colors { get; set; }
}
