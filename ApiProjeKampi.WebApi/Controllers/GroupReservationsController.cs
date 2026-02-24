using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.GroupReservationDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReservationsController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public GroupReservationsController(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoriList()
        {
            var GroupReservations = _apiContext.GroupReservations.ToList();
            return Ok(GroupReservations);
        }

        [HttpPost]
        public IActionResult CreateGroupReservation(CreateGroupReservationDto createGroupReservationDto)
        {
            var value = _mapper.Map<GroupReservation>(createGroupReservationDto);
            _apiContext.GroupReservations.Add(value);
            _apiContext.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteGroupReservation(int id)
        {
            var value = _apiContext.GroupReservations.Find(id);
            _apiContext.GroupReservations.Remove(value);
            _apiContext.SaveChanges();
            return Ok("Silindi");
        }

        [HttpGet("GetGroupReservation")]
        public IActionResult GetGroupReservation(int id)
        {
            var GroupReservation = _apiContext.GroupReservations.Find(id);
            return Ok(GroupReservation);
        }

        [HttpPut]
        public IActionResult UpdateGroupReservation(UpdateGroupReservationDto updateGroupReservationDto)
        {
            var value = _mapper.Map<GroupReservation>(updateGroupReservationDto);
            _apiContext.GroupReservations.Update(value);
            _apiContext.SaveChanges();
            return Ok("Güncellendi");
        }
    }
}
