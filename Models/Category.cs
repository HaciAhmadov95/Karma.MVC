using Karma.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.Models;

public class Category : BaseEntity
{
    [Required]
    public string Name { get; set; }
}
