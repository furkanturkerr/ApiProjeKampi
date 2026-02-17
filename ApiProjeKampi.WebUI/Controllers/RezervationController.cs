using System.Text;
using ApiProjeKampi.WebUI.Dtos.RezervationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.Controllers;

public class RezervationController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RezervationController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> RezervationList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Rezervations");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultRezervationDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateRezervation()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRezervation(CreateRezervationDto createRezervationDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createRezervationDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5083/api/Rezervations", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("RezervationList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteRezervation(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5083/api/Rezervations?id=" + id);
        return RedirectToAction("RezervationList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateRezervation(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Rezervations/GetRezervation?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateRezervationDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRezervation(UpdateRezervationDto updateRezervationDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateRezervationDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5083/api/Rezervations", stringContent);
        return RedirectToAction("RezervationList");
    }
}