using Karma.MVC.Models;

namespace Karma.MVC.ViewModels;

public class GetProductVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double DiscountValue { get; set; }
    public string BrandName { get; set; }
    public List<Image> Images { get; set; }
}
