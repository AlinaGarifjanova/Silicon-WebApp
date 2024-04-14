using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class CheckboxValidator : ValidationAttribute
    {
        public override bool IsValid (object? value)
        {
            return value is bool b && b;
        }
    }
}
