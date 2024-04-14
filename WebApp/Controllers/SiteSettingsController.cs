using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class SiteSettingsController : Controller
{
    public IActionResult Theme(string mode)
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(30),
        };

        Response.Cookies.Append("theme", mode, option);

        return Ok();
    }


    //Så att cookie kommer upp, viss tid 
    public IActionResult Consent()
    {
        var option = new CookieOptions
        {
            Expires = DateTime.Now.AddYears(1),
            HttpOnly = true,
            Secure = true
        };

        Response.Cookies.Append("consent", "true", option);

        return Ok();
    }
}
