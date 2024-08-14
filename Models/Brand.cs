using Karma.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.Models;

public class Brand : BaseEntity
{
    [Required]
    public string Name { get; set; }
}
