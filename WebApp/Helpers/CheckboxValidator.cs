using System.ComponentModel.DataAnnotations;

namespace WebApp.Helpers;

public class CheckboxValidator : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return  value is bool b && b;
    }
}
