using Karma.MVC.Models;

namespace Karma.MVC.ViewModels;

public class GetProductDetailVM
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Depth { get; set; }
    public int Weight { get; set; }
    public bool QualityChecking { get; set; }
    public double? DiscountValue { get; set; } = 0;
    public Comment Comment { get; set; }

    public List<Comment> Comments { get; set; }
    public List<Image> Images { get; set; }
    public List<Color> Colors { get; set; }

    public string CategoryName { get; set; }
}
