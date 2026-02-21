using ApiProjeKampi.WebUI.Dtos.AboutDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _AboutDashboardComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _AboutDashboardComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var reponseMessage = await client.GetAsync("http://localhost:5083/api/Abouts");
        if (reponseMessage.IsSuccessStatusCode)
        {
            var jsonData = await reponseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            return View(values);
        }

        return View();
    }
}