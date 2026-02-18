using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebUI.Controllers;

public class AIController : Controller
{
    // GET
    public IActionResult CreateRecipeWithOpenAI()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
    {
        var apiKey = "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";

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestData = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new
                {
                    role = "system",
                    content =
                        "Sen bir restoran için yemek önerileri yapan bir yapay zeka aracısın. Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak."
                },
                new { role = "user", content = prompt }
            },
            temperature = 0.5
        };

        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
            var content = result.choices[0].message.content;
            ViewBag.recipe = content;
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ViewBag.recipe = $"Bir hata oluştu: {response.StatusCode}<br/>Detay: {errorMessage}";
        }
        return View();
    }


    public class OpenAIResponse
    {
        public List<Choice> choices { get; set; }
    }
    
    public class Choice
    {
        public Message message { get; set; }
    }
    
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}