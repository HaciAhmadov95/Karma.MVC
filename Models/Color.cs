using Karma.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karma.MVC.Models;

public class Color : BaseEntity
{
	[Required]
	public string Name { get; set; }
	[NotMapped]
	public bool IsSelected { get; set; }
	public ICollection<Product>? Products { get; set; }
}
