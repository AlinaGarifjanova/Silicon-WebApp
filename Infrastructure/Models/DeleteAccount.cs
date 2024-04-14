using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class DeleteAccount
    {

        [CheckboxValidator(ErrorMessage = "You need to check the checkbox")]
        [Display(Name = "Yes, I want to delete my account")]
        public bool IsDeleted { get; set; }

    }
}
