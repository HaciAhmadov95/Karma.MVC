using Karma.MVC.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.Models;

public class Subscriber : BaseEntity
{
    [Required]
    public string SubscriberEmail { get; set; }
}
