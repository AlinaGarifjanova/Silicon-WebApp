using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ContactRegistrationForm
    {
        public string FullName { get; set; } 

        public string Email { get; set; }

        public string? HiddenSelectInput { get; set; }
        public string? Message { get; set; }
    }
}
