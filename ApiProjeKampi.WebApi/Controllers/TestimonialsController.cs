using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public TestimonialsController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            var values = _apiContext.Testimonials.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial Testimonial)
        {
            _apiContext.Testimonials.Add(Testimonial);
            _apiContext.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteTestimonial(int id)
        {
            var value = _apiContext.Testimonials.Find(id);
            _apiContext.Testimonials.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Silindi");
        }

        [HttpGet("GetTestimonial")]
        public IActionResult GetTestimonial(int id)
        {
            var Testimonial = _apiContext.Testimonials.Find(id);
            return Ok(Testimonial);
        }

        [HttpPut]
        public IActionResult UpdateTestimonial(Testimonial Testimonial)
        {
            var value = _apiContext.Testimonials.Update(Testimonial);
            _apiContext.SaveChanges();
            return Ok("Güncellendi");
        }
    }
}
