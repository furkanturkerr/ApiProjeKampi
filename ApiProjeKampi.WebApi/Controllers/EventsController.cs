using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public EventsController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult EventList()
        {
            var values = _apiContext.YummyEvents.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateEvent(YummyEvent Event)
        {
            _apiContext.YummyEvents.Add(Event);
            _apiContext.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int id)
        {
            var value = _apiContext.YummyEvents.Find(id);
            _apiContext.YummyEvents.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Silindi");
        }

        [HttpGet("GetEvent")]
        public IActionResult GetEvent(int id)
        {
            var Event = _apiContext.YummyEvents.Find(id);
            return Ok(Event);
        }

        [HttpPut]
        public IActionResult UpdateEvent(YummyEvent Event)
        {
            var value = _apiContext.YummyEvents.Update(Event);
            _apiContext.SaveChanges();
            return Ok("Güncellendi");
        }
    }
}
