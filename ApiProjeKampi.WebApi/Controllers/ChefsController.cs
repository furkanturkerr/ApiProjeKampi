using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public ChefsController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult ChefList()
        {
            var value = _apiContext.Chefs.ToList();
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            _apiContext.Chefs.Add(chef);
            _apiContext.SaveChanges();
            return Ok("Şef eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var value = _apiContext.Chefs.Find(id);
            _apiContext.Chefs.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Chef silindi");
        }

        [HttpGet("GetChef")]
        public IActionResult GetChef(int id)
        {
            var value = _apiContext.Chefs.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            _apiContext.Chefs.Update(chef);
            _apiContext.SaveChanges();
            return Ok("Chef güncellendi");
        }
    }
}
