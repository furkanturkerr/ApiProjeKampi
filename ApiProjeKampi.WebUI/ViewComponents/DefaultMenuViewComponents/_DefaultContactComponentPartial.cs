using ApiProjeKampi.WebApi.Dto.ContactDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DefaultMenuViewComponents;

public class _DefaultContactComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DefaultContactComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var reponseMessage = await client.GetAsync("http://localhost:5083/api/Contacts");
        if (reponseMessage.IsSuccessStatusCode)
        {
            var jsonData = await reponseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);
            return View(values);
        }

        return View();
    }
}