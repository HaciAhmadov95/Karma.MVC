using Karma.MVC.Models.Base;

namespace Karma.MVC.Models;

public class BlogCategory : BaseEntity
{
    public string Name { get; set; }
    public List<Blog> Blogs { get; set; }
}
