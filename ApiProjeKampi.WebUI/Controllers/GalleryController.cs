using System.Text;
using ApiProjeKampi.WebApi.Dtos.ImagesDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CreateImageDto = ApiProjeKampi.WebUI.Dtos.ImageDtos.CreateImageDto;
using ResultImageDto = ApiProjeKampi.WebUI.Dtos.ImageDtos.ResultImageDto;
using UpdateImageDto = ApiProjeKampi.WebUI.Dtos.ImageDtos.UpdateImageDto;

namespace ApiProjeKampi.WebUI.Controllers;

public class GalleryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GalleryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    // GET
    public async Task<IActionResult> ImageList()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Images");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);
            return View(values);
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> ImageListWithEdit()
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Images");
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);
            return View(values);
        }
        return View();
    }

    public IActionResult CreateImage()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateImage(CreateImageDto createImageDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createImageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5083/api/Images", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ImageListWithEdit");
        }
        return View();
    }

    public async Task<IActionResult> DeleteImage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5083/api/Images?id=" + id);
        return RedirectToAction("ImageListWithEdit");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateImage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.GetAsync("http://localhost:5083/api/Images/GetImage?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateImageDto>(jsonData);
            return View(values);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateImage(UpdateImageDto updateImageDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateImageDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5083/api/Images", stringContent);
        return RedirectToAction("ImageListWithEdit");
    }
}