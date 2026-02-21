using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents;

public class _DashboardWidgetsComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardWidgetsComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        int r1, r2, r3, r4;
        Random rnd = new();
        r1 = rnd.Next(1, 35);
        r2 = rnd.Next(1, 35);
        r3 = rnd.Next(1, 35);
        r4 = rnd.Next(1, 35);
        
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Rezervations/GetTotalRezervationCount");
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        ViewBag.v1 = jsonData;
        ViewBag.r1 = r1;
        
        var client1 = _httpClientFactory.CreateClient();
        var responseMessage1 = await client1.GetAsync("http://localhost:5083/api/Rezervations/GetTotalCustomerCount");
        var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
        ViewBag.v2 = jsonData1;
        ViewBag.r2 = r2;
        
        var client2 = _httpClientFactory.CreateClient();
        var responseMessage2 = await client2.GetAsync("http://localhost:5083/api/Rezervations/GetPendingRezervation");
        var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
        ViewBag.v3 = jsonData2;
        ViewBag.r3 = r3;
        
        var client3 = _httpClientFactory.CreateClient();
        var responseMessage3 = await client3.GetAsync("http://localhost:5083/api/Rezervations/GetApprovedRezervation");
        var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
        ViewBag.v4 = jsonData3;
        ViewBag.r4 = r4;
        
        return View();
    }
}