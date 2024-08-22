namespace Karma.MVC.ViewModels;

public class CartProductVM
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public double? DiscountValue { get; set; } = 0;

    public double TotalPrice { get; set; }
    public ICollection<ImageVM> Images { get; set; }
}
