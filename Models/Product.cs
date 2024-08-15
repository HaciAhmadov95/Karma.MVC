using Karma.MVC.Models.Base;
using Karma.MVC.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karma.MVC.Models;

public class Product : BaseEntity
{
    [Required, MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required, Range(0, 100000000)]
    public double Price { get; set; }
    [Required, Range(0, 10000)]
    public int Width { get; set; }
    [Required, Range(0, 10000)]
    public int Height { get; set; }
    [Required, Range(0, 10000)]
    public int Depth { get; set; }
    [Required, Range(0, 10000)]
    public int Weight { get; set; }
    [Required]
    public bool QualityChecking { get; set; }
    public double? DiscountValue { get; set; } = 0;

    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [NotMapped]
    public IFormFile MainImage { get; set; }
    [NotMapped]
    public List<IFormFile> ImageFile { get; set; }

    public ICollection<Comment> Comments { get; set; }
    public ICollection<Wishlist> Wishlists { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<Cart> Carts { get; set; }
    public ICollection<Color> Colors { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; }
}
