using System.Net.Http.Headers;
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

    [HttpGet]
    public async Task<IActionResult> AnswerMessageWithOpenAI(int id, string prompt)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Messages/GetMessage?id=" + id);
        var jsonData = await responseMessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);
        prompt = values.MessageDetails;
        
        var apiKey = "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";

        using var client2 = new HttpClient();
        client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content =
                        "Sen bir restorant için kullanıcıarın göndermiş oldukları mesajları detaylı ve olabildiğince olumlu, müşteri memnuniyeti gözeten cevaplar veren bir yapay zeka aracısın. Amacımız kullanıcı tarafından gönderilen mesajlara en olumlu ve en mantıklı cevapları sunabilmek."
                },
                new { role = "user", content = prompt }
            },
            temperature = 0.5
        };

        var response = await client2.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<AIController.OpenAIResponse>();
            var content = result.choices[0].message.content;
            ViewBag.ancwerAI = content;
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.ancwerAI = $"Bir hata oluştu: {response.StatusCode}<br/>Detay: {errorMessage}";
        }
        return View(values);
    }
}