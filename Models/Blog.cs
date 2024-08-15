using Karma.MVC.Models.Base;
using Karma.MVC.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karma.MVC.Models;

public class Blog : BaseEntity
{
    [Required]
    public string Title { get; set; }
    public string Content { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    [NotMapped]
    public IFormFile MainFile { get; set; }

    [NotMapped]
    public List<IFormFile> ImageFile { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public ICollection<Image> Images { get; set; }
}
