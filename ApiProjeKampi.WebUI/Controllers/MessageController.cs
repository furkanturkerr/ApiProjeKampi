using System.Text;
using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.Controllers;

public class MessageController : Controller
{
     private readonly IHttpClientFactory _httpClientFactory;

    public MessageController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> MessageList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Messages");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateMessage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createMessageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5083/api/Messages", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5083/api/Messages?id=" + id);
        return RedirectToAction("MessageList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateMessage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Messages/GetMessage?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateMessageDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateMessageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5083/api/Messages", stringContent);
        return RedirectToAction("MessageList");
    }
}