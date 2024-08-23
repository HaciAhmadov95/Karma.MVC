namespace Karma.MVC.ViewModels;

public class ProductListViewModel
{
	public List<GetProductVM> Products { get; set; }
	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
}
