using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiProjeKampi.WebUI.Controllers;

public class MessageController : Controller
{
     private readonly IHttpClientFactory _httpClientFactory;
     private readonly IConfiguration _configuration;

    public MessageController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
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
        
        var apiKey = _configuration["ApiKeys:OpenAI"];

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

    public PartialViewResult SendMessage()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
    {
        var client = new HttpClient();
        var apiKey = _configuration["ApiKeys:HuggingFace"];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        try
        {
            var translateRequestBody = new
            {
                inputs = createMessageDto.MessageDetails
            };
            var translateJson = JsonSerializer.Serialize(translateRequestBody);
            var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");
            
            var translateResponse = await client.PostAsync("https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-tr-en", translateContent);
            var tranlateResponseString = await translateResponse.Content.ReadAsStringAsync();
            
            string englishtext = createMessageDto.MessageDetails;
            if (tranlateResponseString.TrimStart().StartsWith("["))
            {
                var translateDock = JsonDocument.Parse(tranlateResponseString);
                englishtext = translateDock.RootElement[0].GetProperty("translation_text").GetString();
                
                ViewBag.v = englishtext;
            }
            
            var toxicRequestBody = new
            {
                inputs = englishtext
            };
            var toxicJson = JsonSerializer.Serialize(toxicRequestBody);
            var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
            
            var toxicResponse = await client.PostAsync("https://api-inference.huggingface.co/models/deepset/sentence_bert", toxicContent);
            var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();
            if (toxicResponseString.TrimStart().StartsWith("["))
            {
                var toxicDock = JsonDocument.Parse(toxicResponseString);
                foreach (var item in toxicDock.RootElement[0].EnumerateArray())
                {
                    string label = item.GetProperty("label").GetString();
                    double score = item.GetProperty("score").GetDouble();

                    if (score > 0.5)
                    {
                        createMessageDto.Status = "Toksik Mesaj!";
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(createMessageDto.Status))
            {
                createMessageDto.Status = "Mesaj Alındı!";
            }
           
        }
        catch (Exception ex)
        {
            
            createMessageDto.Status = "Onay Bekliyor";
        }
        
        var client2 = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createMessageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client2.PostAsync("http://localhost:5083/api/Messages", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }
}