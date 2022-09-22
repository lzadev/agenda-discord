using Agenda.DTOs;
using Agenda.Exceptions;
using Agenda.Models;
using Agenda.Repository.Abstract;
using Agenda.Services.Abstract;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Services.Concret
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ContactDto>>(await _contactRepository.GetAll());
        }

        public async Task<ContactDto> GetById(int id)
        {
            var contact = await _contactRepository.GetById(id);
            ValidateIfContactIsNull(id, contact);
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> Create(CreateContactDto model)
        {
            var contact = await _contactRepository.Create(_mapper.Map<Contact>(model));
            return _mapper.Map<ContactDto>(contact);
        }

        public void Delete(int id)
        {
            var contact = Task.Run(async () => await _contactRepository.GetById(id)).Result;
            ValidateIfContactIsNull(id, contact);
            contact.IsDeleted = true;
            _contactRepository.Delete(contact);
        }

        public async Task<ContactDto> Update(int id, UpdateContactDto model)
        {
            if (id != model.Id) throw new BadRequestException("The query Id does not match with the Id provided in the body request");
            var contact = Task.Run(async () => await _contactRepository.GetById(id)).Result;
            ValidateIfContactIsNull(id, contact);
            var contactUpdated = await _contactRepository.Update(_mapper.Map(model, contact));
            return _mapper.Map<ContactDto>(contact);
        }

        private void ValidateIfContactIsNull(int id, Contact contact)
        {
            if (contact == null) throw new NotFoundException($"No contact was found with id {id}");
        }
    }
}
