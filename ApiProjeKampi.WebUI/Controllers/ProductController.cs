using System.Text;
using ApiProjeKampi.WebUI.Dtos.CategoryDtos;
using ApiProjeKampi.WebUI.Dtos.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.Controllers;

public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> ProductList()
    {
        var client = _httpClientFactory.CreateClient();

        // Ürünleri çek
        var productResponse = await client.GetAsync("http://localhost:5083/api/Products/GetProductWithProduct");
        List<ResultProductDto> products = new();
        if (productResponse.IsSuccessStatusCode)
        {
            var jsonData = await productResponse.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
        }

        // Kategorileri çek
        var categoryResponse = await client.GetAsync("http://localhost:5083/api/Categories");
        List<ResultCategoryDto> categories = new();
        if (categoryResponse.IsSuccessStatusCode)
        {
            var jsonData = await categoryResponse.Content.ReadAsStringAsync();
            categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
        }

        ViewBag.Categories = categories;

        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        var client = _httpClientFactory.CreateClient();
        var responsemessage = await client.GetAsync("http://localhost:5083/api/Categories");
        
        var jsonData = await responsemessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
        List<SelectListItem> value2 = (from x in values 
            select new SelectListItem
            {
                Text = x.Name, 
                Value = x.CategoryId.ToString()
            }).ToList();
        ViewBag.Categories = value2;
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(createProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("http://localhost:5083/api/Products", stringContent);
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ProductList", "Product");
        }
        return View(createProductDto);
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var client = _httpClientFactory.CreateClient();
        await client.DeleteAsync("http://localhost:5083/api/Products?id=" + id);
        return RedirectToAction("ProductList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var responsemessage = await client.GetAsync("http://localhost:5083/api/Categories");
        
        var jsonData = await responsemessage.Content.ReadAsStringAsync();
        var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
        List<SelectListItem> value2 = (from x in values 
            select new SelectListItem
            {
                Text = x.Name, 
                Value = x.CategoryId.ToString()
            }).ToList();
        ViewBag.Categories = value2;
        
        var client2 = _httpClientFactory.CreateClient();
        var responseMessage = await client2.GetAsync("http://localhost:5083/api/Products/GetProduct?id=" + id);
        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData2 = await responseMessage.Content.ReadAsStringAsync();
            var values2 = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData2);
            return View(values2);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(updateProductDto);
        StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
        await client.PutAsync("http://localhost:5083/api/Products", stringContent);
        return RedirectToAction("ProductList");
    }
}