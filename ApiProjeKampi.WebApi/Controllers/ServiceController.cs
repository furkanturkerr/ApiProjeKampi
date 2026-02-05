using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public ServiceController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult ServiceList()
        {
            var values = _apiContext.Services.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            _apiContext.Services.Add(service);
            _apiContext.SaveChanges();
            return Ok("Servis eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteService(int id)
        {
            var value = _apiContext.Services.Find(id);
            _apiContext.Services.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Servis silindi");
        }

        [HttpGet("GetService")]
        public IActionResult GetService(int id)
        {
            var service = _apiContext.Services.Find(id);
            return Ok(service);
        }

        [HttpPut]
        public IActionResult UpdateService(Service service)
        {
            var value = _apiContext.Services.Update(service);
            _apiContext.SaveChanges();
            return Ok("Servis güncellendi");
        }
    }
}
