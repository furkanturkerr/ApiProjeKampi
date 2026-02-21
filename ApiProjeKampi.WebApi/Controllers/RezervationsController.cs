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
        
        [HttpGet("GetTotalRezervationCount")]
        public IActionResult GetTotalRezervationCount()
        {
            var value = _context.Rezervations.Count();
            return Ok(value);
        }
        
        [HttpGet("GetTotalCustomerCount")]
        public IActionResult GetTotalCustomerCount()
        {
            var value = _context.Rezervations.Sum(x => x.CountOfPeople);
            return Ok(value);
        }
        
        [HttpGet("GetPendingRezervation")]
        public IActionResult GetPendingRezervation()
        {
            var value = _context.Rezervations
                .Where(x => x.Status == Rezervation.ReservationStatus.OnayBekliyor)
                .Count();

            return Ok(value);
        }

        [HttpGet("GetApprovedRezervation")]
        public IActionResult GetApprovedRezervation()
        {
            var value = _context.Rezervations
                .Where(x => x.Status == Rezervation.ReservationStatus.Onaylandi)
                .Count();

            return Ok(value);
        }
        
        [HttpGet("GetReservationStats")]
        public IActionResult GetReservationStats()
        {
            DateTime today = DateTime.Today;
            DateTime fourMonthsAgo = today.AddMonths(-3);

            // Önce veritabanından gruplama
            var rawData = _context.Rezervations
                .Where(r => r.Date >= fourMonthsAgo)
                .GroupBy(r => new { r.Date.Year, r.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Approved = g.Count(x => x.Status == Rezervation.ReservationStatus.Onaylandi),
                    Pending = g.Count(x => x.Status == Rezervation.ReservationStatus.OnayBekliyor),
                    Canceled = g.Count(x => x.Status == Rezervation.ReservationStatus.IptalEdildi)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            // Bellekte string dönüşümü
            var result = rawData.Select(x => new ReservationChartDto
            {
                Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"),
                Approved = x.Approved,
                Pending = x.Pending,
                Canceled = x.Canceled
            }).ToList();

            return Ok(result);
        }
    }
}
