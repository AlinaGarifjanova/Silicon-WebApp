using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AccountUser
    {
        [Display(Name = "First Name", Prompt = "Enter your first name")]
        [Required(ErrorMessage = "A first name is required")]
        [MinLength(2, ErrorMessage = "A valid first name is required")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name", Prompt = "Enter your last name")]
        [Required(ErrorMessage = "A last name is required")]
        [MinLength(2, ErrorMessage = "A valid last name is required")]
        public string LastName { get; set; } = null!;

      
        [Display(Name = "Phone number", Prompt = "Enter your phone number")]
        [DataType(DataType.PhoneNumber, ErrorMessage ="You can only use numbers")]
        public string? Phone { get; set; }
      
        [Display(Name = "Bio (Optional)", Prompt = "Add a short bio...")]
        public string? Bio { get; set; }
     
    }
}
