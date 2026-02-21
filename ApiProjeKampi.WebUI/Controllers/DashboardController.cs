using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class DashboardController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}