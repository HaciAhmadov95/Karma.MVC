using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.ViewModels;

public class LoginVM
{
    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required, MaxLength(50), DataType(DataType.Password)]
    public string Password { get; set; }
}
