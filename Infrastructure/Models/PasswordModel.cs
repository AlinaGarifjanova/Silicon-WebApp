using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PasswordModel
    {

        // public string UserId { get; set; }

        [Display(Name = "Current Password", Prompt = "Enter your old password")]
        [MinLength(8, ErrorMessage = "A password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = null!;


        [Display(Name = "New Password", Prompt = "Enter your new password")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_])(?!.*\s).{8,}$", ErrorMessage = "A valid password is required")]
        [MinLength(8, ErrorMessage = "A password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = null!;

        [Display(Name = "Confirm New Password", Prompt = "Confirm your new password")]
        [Required(ErrorMessage = "Password needs to be confirmed")]
        [Compare(nameof(NewPassword), ErrorMessage = "The confirmed password doesn't match your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
