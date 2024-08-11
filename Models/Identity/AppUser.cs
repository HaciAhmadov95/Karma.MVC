using Microsoft.AspNetCore.Identity;

namespace Karma.MVC.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
