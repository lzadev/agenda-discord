using Agenda.DTOs;
using Agenda.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : Controller
    {
        private readonly IContactService _contactService;

        public AgendaController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactDto>> GetAll() => await _contactService.GetAll();

        [HttpGet("{id:int}")]
        public async Task<ContactDto> GetById(int id) => await _contactService.GetById(id);

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _contactService.Delete(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ContactDto> Update(int id, UpdateContactDto model) => await _contactService.Update(id, model);

        [HttpPost]
        public async Task<ContactDto> Create(CreateContactDto model) => await _contactService.Create(model);
    }
}
