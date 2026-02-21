using ApiProjeKampi.WebApi.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public StatisticsController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            var value = _apiContext.Products.Count();
            return Ok(value);
        }
        
        [HttpGet("RezervationCount")]
        public IActionResult RezervationCount()
        {
            var value = _apiContext.Rezervations.Count();
            return Ok(value);
        }
        
        [HttpGet("ChefCount")]
        public IActionResult ChefCount()
        {
            var value = _apiContext.Chefs.Count();
            return Ok(value);
        }
        
        [HttpGet("TotalGuestCount")]
        public IActionResult TotalGuestCount()
        {
            var value = _apiContext.Rezervations.Sum(x=>x.CountOfPeople);
            return Ok(value);
        }
    }
}
