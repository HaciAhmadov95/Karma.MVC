using Karma.MVC.Models.Identity;

namespace Karma.MVC.Models;

public class Image
{
    public int Id { get; set; }
    public string Url { get; set; }
    public bool? IsMain { get; set; }
    public AppUser? AppUser { get; set; }
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    public int? BlogId { get; set; }
    public Blog? Blog { get; set; }
}
