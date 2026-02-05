using System.Text.Json.Serialization;
using ApiProjeKampi.WebUI.Dtos.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents;

public class _ServiceDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ServiceDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var reponseMessage = await client.GetAsync("http://localhost:5083/api/Service");
        if (reponseMessage.IsSuccessStatusCode)
        {
            var jsonData = await reponseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}