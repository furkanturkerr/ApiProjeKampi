using System.Text;
using ApiProjeKampi.WebUI.Dtos.WhyYummyDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.Controllers;

public class WhyYummyController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WhyYummyController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> WhyYummyList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Service");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultWhyYummyDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateWhyYummy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateWhyYummy(CreateWhyYummyDto createWhyYummyDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createWhyYummyDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5083/api/Service", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("WhyYummyList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteWhyYummy(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5083/api/Service?id=" + id);
        return RedirectToAction("WhyYummyList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateWhyYummy(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Service/GetService?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateWhyYummyDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateWhyYummy(UpdateWhyYummyDto updateWhyYummyDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateWhyYummyDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5083/api/Service", stringContent);
        return RedirectToAction("WhyYummyList");
    }
}