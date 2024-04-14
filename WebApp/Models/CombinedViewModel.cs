using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class CombinedViewModel
{
    [Display(Name = "Email address", Prompt = "Enter your email address")]
    public string? Email { get; set; } 
    public AddressUser? Address { get; set; }
    public AccountUser? Basic { get; set; }

}


