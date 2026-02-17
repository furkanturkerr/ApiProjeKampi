using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _NavbarMessgaeListAdminLayoutComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _NavbarMessgaeListAdminLayoutComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Messages");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(jsonData);
            return View(values);
        }
        return View();
    }
}