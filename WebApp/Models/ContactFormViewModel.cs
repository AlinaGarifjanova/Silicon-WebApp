using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ContactFormViewModel
    {
        public Contact Contact { get; set; } = new Contact();
        public IEnumerable<Option>? Options { get; set; } = new List<Option>();
       
      
    }
}

