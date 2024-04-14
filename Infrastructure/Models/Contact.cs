using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "A valid full name is required")]
        [MinLength(5, ErrorMessage = "You need to enter a valid full name")]
        [Display(Name = "Full name", Prompt = "Enter your full name")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Email address", Prompt = "Enter your email address")]
        [Required(ErrorMessage = "An email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Message", Prompt = "Message")]
        public string? Message { get; set; }


        [Display(Name = "All Service", Prompt = "Choose a service")]
        public string? HiddenSelectInput { get; set; }
    }
}
