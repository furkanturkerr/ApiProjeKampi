using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class AdminLayoutController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}