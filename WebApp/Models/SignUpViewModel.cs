using System.ComponentModel.DataAnnotations;
using WebApp.Filters;
using WebApp.Helpers;

namespace WebApp.Models;

public class SignUpViewModel
{
    [Display (Name ="First Name", Prompt="Enter your first name")]
    [Required(ErrorMessage ="A first name is required")]
    [MinLength(2,ErrorMessage = "A valid first name is required")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    [Required(ErrorMessage = "A last name is required")]
    [MinLength(2,ErrorMessage = "A valid last name is required")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Email address", Prompt = "Enter your email address")]
    [Required(ErrorMessage = "An email is required")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,}$", ErrorMessage = "An valid email address is required")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password")]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$", ErrorMessage = "A valid password is required")]
    [MinLength(8,ErrorMessage = "A password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [Required(ErrorMessage = "Password needs to be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "The confirmed password doesn't match your password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;

    [CheckboxValidator(ErrorMessage = "You need to accept the terms")]
    [Display(Name = "I agree to the Terms & Conditions")]
    public bool TermsAndConditions { get; set; }
}
