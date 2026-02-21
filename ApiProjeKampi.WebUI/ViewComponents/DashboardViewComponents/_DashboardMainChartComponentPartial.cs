using ApiProjeKampi.WebUI.Dtos.RezervationDtos;
using ApiProjeKampi.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiProjeKampi.WebUI.ViewComponents.DashboardViewComponents;

public class _DashboardMainChartComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _DashboardMainChartComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri("http://localhost:5083/");

        var response = await client.GetAsync("api/Rezervations/GetReservationStats");

        if (!response.IsSuccessStatusCode)
        {
            return View(new List<ReservationChartDto>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<List<ReservationChartDto>>(json);

        return View(data ?? new List<ReservationChartDto>());
    }
}