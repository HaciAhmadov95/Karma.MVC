using System.ComponentModel.DataAnnotations;

namespace Karma.MVC.Models;

public class Subscriber
{
    public int Id { get; set; }

    [Required]
    public string SubscriberEmail { get; set; }
}
