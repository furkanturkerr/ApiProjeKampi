using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class DefaultController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}