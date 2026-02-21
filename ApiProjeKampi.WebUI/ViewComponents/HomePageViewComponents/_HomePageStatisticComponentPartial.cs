using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.ViewComponents.HomePageViewComponents;

public class _HomePageStatisticComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _HomePageStatisticComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client1 = _httpClientFactory.CreateClient();
        var reponseMessage1 = await client1.GetAsync("http://localhost:5083/api/Statistics/ProductCount");
        var jsonData1 = await reponseMessage1.Content.ReadAsStringAsync();
        ViewBag.v1 = jsonData1;
        
        var client2 = _httpClientFactory.CreateClient();
        var reponseMessage2 = await client2.GetAsync("http://localhost:5083/api/Statistics/RezervationCount");
        var jsonData2 = await reponseMessage2.Content.ReadAsStringAsync();
        ViewBag.v2 = jsonData2;
        
        var client3 = _httpClientFactory.CreateClient();
        var reponseMessage3 = await client3.GetAsync("http://localhost:5083/api/Statistics/ChefCount");
        var jsonData3 = await reponseMessage3.Content.ReadAsStringAsync();
        ViewBag.v3 = jsonData3;
        
        var client4 = _httpClientFactory.CreateClient();
        var reponseMessage4 = await client4.GetAsync("http://localhost:5083/api/Statistics/TotalGuestCount");
        var jsonData4 = await reponseMessage4.Content.ReadAsStringAsync();
        ViewBag.v4 = jsonData4;

        return View();
    }
}