using Agenda.DTOs;
using Agenda.Exceptions;
using Agenda.Services.Abstract;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ApiResponse<ContactDto>> GetAll() => await _contactService.GetAll();

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<ContactDto>> GetById(int id) => await _contactService.GetById(id);

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse<bool>> Delete(int id) => await _contactService.Delete(id);

        [HttpPut("{id:int}")]
        public async Task<ApiResponse<ContactDto>> Update(int id, UpdateContactDto model) => await _contactService.Update(id, model);


        [HttpPatch("{id:int}")]
        public async Task<ApiResponse<ContactDto>> Update(JsonPatchDocument<UpdateContactDto> contact) {

            if (contact == null) throw new BadRequestException("Algo salió mal");
            contact.ApplyTo()
            await _contactService.Update(contact);
        }

        [HttpPost]
        public async Task<ApiResponse<ContactDto>> Create(CreateContactDto model) => await _contactService.Create(model);
    }
}
