using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class DefaultController(DataContext context) : Controller
{
    private readonly DataContext _context = context;

    [Route("/")]
    public IActionResult Home()
    {
        return View();
    }

}
