using Karma.MVC.Models;

namespace Karma.MVC.ViewModels;

public class BlogDetailVM
{
	public Blog Blog { get; set; }
	public List<BlogCategory> BlogCategories { get; set; }
	public List<Blog> PopularBlogs { get; set; }
}
