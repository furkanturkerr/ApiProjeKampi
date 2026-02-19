using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class ChatController : Controller
{
    // GET
    public IActionResult SendChatWithAI()
    {
        return View();
    }
}