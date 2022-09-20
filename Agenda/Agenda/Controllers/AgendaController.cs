using Agenda.Models;
using Agenda.Repository.Concret;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public AgendaController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAll()
        {
            var contacts = await _contactRepository.GetAll();

            return contacts;
        }

        [HttpGet("{id:int}")]
        public async Task<Contact> GetById(int id)
        {
            var contact = await _contactRepository.GetById(id);

            return contact;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactRepository.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.IsDeleted = true;

            if (!await _contactRepository.Delete(contact))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Contact model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var contact = await _contactRepository.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            contact.Id = contact.Id;
            contact.Name = model.Name;
            contact.LastName = model.LastName;
            contact.Address = model.Address;
            contact.PhoneNumber = model.PhoneNumber;
            await _contactRepository.Update(contact);

            return Ok(contact);
        }

        [HttpPost]
        public async Task<Contact> Create(Contact model)
        {
            var contact = await _contactRepository.Create(model);

            return contact;
        }
    }
}
