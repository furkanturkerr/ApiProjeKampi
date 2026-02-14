using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class CategoryController : Controller
{
    // GET
    public IActionResult CategoryList()
    {
        return View();
    }
}