using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karma.MVC.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Product>? Products { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
