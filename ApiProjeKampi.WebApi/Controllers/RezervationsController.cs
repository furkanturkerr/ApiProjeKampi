using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.RezervationDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public RezervationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult RezervationList()
        {
            var values = _context.Rezervations.ToList();
            return Ok(_mapper.Map<List<ResultRezervationDto>>(values));
        }

        [HttpPost]
        public IActionResult CreateRezervation(CreateRezervationDto createRezervationDto)
        {
            var value = _mapper.Map<Rezervation>(createRezervationDto);
            _context.Rezervations.Add(value);
            _context.SaveChanges();
            return Ok("Ekleme işlemi yapıldı");
        }

        [HttpDelete]
        public IActionResult DeleteRezervation(int id)
        {
            var value = _context.Rezervations.Find(id);
            _context.Rezervations.Remove(value);
            _context.SaveChanges();
            return Ok("Silme işlemi yapıldı.");
        }

        [HttpGet("GetRezervation")]
        public IActionResult GetRezervation(int id)
        {
            var value = _context.Rezervations.Find(id);
            return Ok(_mapper.Map<GetRezervationByIdDto>(value));
        }
        
        [HttpPut]
        public IActionResult UpdateRezervation(UpdateRezervationDto updateRezervationDto)
        {
            var valeu = _mapper.Map<Rezervation>(updateRezervationDto);
            _context.Rezervations.Update(valeu);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi yapıldı");
        }
    }
}
