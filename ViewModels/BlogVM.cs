using Karma.MVC.Models;

namespace Karma.MVC.ViewModels;

public class BlogVM
{
    public List<Blog> Blogs { get; set; }
    public List<BlogCategory> BlogCategories { get; set; }
    public List<Blog> PopularBlogs { get; set; }
}
