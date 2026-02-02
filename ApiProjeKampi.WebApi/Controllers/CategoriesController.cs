using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _apiContext;

        public CategoriesController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        [HttpGet]
        public IActionResult CategoriList()
        {
            var categories = _apiContext.Categories.ToList();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _apiContext.Categories.Add(category);
            _apiContext.SaveChanges();
            return Ok("Kategori eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var value = _apiContext.Categories.Find(id);
            _apiContext.Categories.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Kategori silindi");
        }

        [HttpGet("GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var category = _apiContext.Categories.Find(id);
            return Ok(category);
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var value = _apiContext.Categories.Update(category);
            _apiContext.SaveChanges();
            return Ok("Kategori güncellendi");
        }
    }
}
