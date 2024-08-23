namespace Karma.MVC.ViewModels;

public class HomeVM
{
    public List<GetProductVM> SliderProducts { get; set; }
    public List<GetProductVM> BrandsProducts { get; set; }
    public List<GetProductVM> BannerProducts { get; set; }
    public List<GetProductVM> DiscountedProducts { get; set; }
}
