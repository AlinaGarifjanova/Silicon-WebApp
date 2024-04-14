using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AddressUser
    {
        [Required(ErrorMessage = "A valid addressline is required")]
        [Display(Name = "AddressLine 1", Prompt = "Enter your adressline")]
        public string AddressLine1 { get; set; } = null!;

        [Display(Name = "AddressLine 2", Prompt = "Enter your second adressline")]
        public string? AddressLine2 { get; set; }
      
        [Required(ErrorMessage = "A valid postalcode is required")]
        [Display(Name = "Postal Code", Prompt = "Enter your postal code")]
        public string PostalCode { get; set; } = null!;
        
        [Required(ErrorMessage = "A valid city is required")]
        [Display(Name = "City", Prompt = "Enter your city")]
        public string City { get; set; } = null!;
    }
}
