using Karma.MVC.Models.Base;
using Karma.MVC.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.Models;

public class Comment : BaseEntity
{
    [Required, MaxLength(255)]
    public string Content { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    public int? BlogId { get; set; }
    public Blog? Blog { get; set; }
}
