using System.Text;
using ApiProjeKampi.WebUI.Dtos.YummyEventsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.Controllers;

public class YummyEventController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public YummyEventController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> YummyEventList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Events");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultYummyEventsDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateYummyEvent()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateYummyEvent(CreateYummyEventsDto createYummyEventDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createYummyEventDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5083/api/Events", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("YummyEventList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteYummyEvent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5083/api/Events?id=" + id);
        return RedirectToAction("YummyEventList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateYummyEvent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Events/GetEvent?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateYummyEventsDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateYummyEvent(UpdateYummyEventsDto updateYummyEventDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateYummyEventDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5083/api/Events", stringContent);
        return RedirectToAction("YummyEventList");
    }
}