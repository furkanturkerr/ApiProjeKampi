using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.EmployeeTaskDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public EmployeeTasksController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult EmployeeTaskList()
        {
            var values = _context.EmployeeTasks.ToList();
            return Ok(_mapper.Map<List<ResultEmployeeTaskDto>>(values));
        }

        [HttpPost]
        public IActionResult CreateEmployeeTask(CreateEmployeeTaskDto createEmployeeTaskDto)
        {
            var value = _mapper.Map<EmployeeTask>(createEmployeeTaskDto);
            _context.EmployeeTasks.Add(value);
            _context.SaveChanges();
            return Ok("Ekleme işlemi yapıldı");
        }

        [HttpDelete]
        public IActionResult DeleteEmployeeTask(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            _context.EmployeeTasks.Remove(value);
            _context.SaveChanges();
            return Ok("Silme işlemi yapıldı.");
        }

        [HttpGet("GetEmployeeTask")]
        public IActionResult GetEmployeeTask(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            return Ok(_mapper.Map<GetEmployeeTaskByIdDto>(value));
        }
        
        [HttpPut]
        public IActionResult UpdateEmployeeTask(UpdateEmployeeTaskDto updateEmployeeTaskDto)
        {
            var valeu = _mapper.Map<EmployeeTask>(updateEmployeeTaskDto);
            _context.EmployeeTasks.Update(valeu);
            _context.SaveChanges();
            return Ok("Güncelleme işlemi yapıldı");
        }
    }
}
