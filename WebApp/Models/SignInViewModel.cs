using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SignInViewModel
{

    [Display(Name = "Email address", Prompt = "Enter your email")]
    [Required(ErrorMessage = "A valid email is required")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter your password")]
    [Required(ErrorMessage = "A valid password is required")]
    [MinLength(8, ErrorMessage = "A valid password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;


    [Display(Name ="Keep me signed in")]
    public bool RememberMe { get; set; } 
}
