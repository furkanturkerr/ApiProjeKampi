using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public ProductsController(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoriList()
        {
            var Products = _apiContext.Products.ToList();
            return Ok(Products);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Product>(createProductDto);
            _apiContext.Products.Add(value);
            _apiContext.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var value = _apiContext.Products.Find(id);
            _apiContext.Products.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Silindi");
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var Product = _apiContext.Products.Find(id);
            return Ok(Product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Product>(updateProductDto);
            _apiContext.Products.Update(value);
            _apiContext.SaveChanges();
            return Ok("Güncellendi");
        }
        
        [HttpGet("GetProductWithProduct")]
        public IActionResult GetProductWithProduct()
        {
            var value = _apiContext.Products.Include(x=>x.Category).ToList();
            return Ok(_mapper.Map<List<ResultProductWithProductDto>>(value));
        }
    }
}
