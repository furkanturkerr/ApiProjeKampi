using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _AboutDashboardComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}