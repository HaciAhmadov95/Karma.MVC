using Karma.MVC.Helpers;
using Karma.MVC.Models;

namespace Karma.MVC.ViewModels;

public class ShopVM
{
    public Paginate<Product> PaginateProducts { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public List<Category> Categories { get; set; }
    public List<Brand> Brands { get; set; }
    public List<Color> Colors { get; set; }

    public int Page { get; set; } = 1;
    public int Take { get; set; } = 6;
    public string? SearchText { get; set; }
}
