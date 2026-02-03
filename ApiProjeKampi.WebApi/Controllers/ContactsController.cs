using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dto.ContactDto;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;
        public ContactsController(ApiContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult ContactList()
        {
            return Ok(_context.Contacts.ToList());
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            Contact contact = new Contact();
            contact.Email = createContactDto.Email;
            contact.Address = createContactDto.Address;
            contact.Map = createContactDto.Map;
            contact.OpenHours = createContactDto.OpenHours;
            contact.Phone = createContactDto.Phone;
            _context.Add(contact);
            _context.SaveChanges();
            return Ok("Eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            var value = _context.Contacts.Find(id);
            _context.Contacts.Remove(value);
            _context.SaveChanges();
            return Ok("Silindi");
        }

        [HttpGet]
        public IActionResult GetContact(int id)
        {
            var value = _context.Contacts.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            Contact contact = new Contact();
            contact.Email = updateContactDto.Email;
            contact.Address = updateContactDto.Address;
            contact.Map = updateContactDto.Map;
            contact.OpenHours = updateContactDto.OpenHours;
            contact.Phone = updateContactDto.Phone;
            contact.ContactId = updateContactDto.ContactId;
            _context.Update(contact);
            _context.SaveChanges();
            return Ok("Güncellendi");
        }
    }
}
