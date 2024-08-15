using Karma.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.Models;

public class Color : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public ICollection<Product>? Products { get; set; }
}
