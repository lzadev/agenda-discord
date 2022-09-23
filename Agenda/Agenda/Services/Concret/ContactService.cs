using Agenda.DTOs;
using Agenda.Exceptions;
using Agenda.Models;
using Agenda.Repository.Abstract;
using Agenda.Services.Abstract;
using AutoMapper;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Services.Concret
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateContactDto> _createContactValidator;

        public ContactService(IContactRepository contactRepository, IMapper mapper, IValidator<CreateContactDto> createContactValidator)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _createContactValidator = createContactValidator;
        }

        public async Task<ApiResponse<ContactDto>> GetAll()
        {
            var apiResponse = new ApiResponse<ContactDto>();
            var contacts = _mapper.Map<IEnumerable<ContactDto>>(await _contactRepository.GetAll());
            apiResponse.result = new ResultBody<ContactDto> { items = contacts.ToList(), totalAcount = contacts.Count() };
            return apiResponse;
        }

        public async Task<ApiResponse<ContactDto>> GetById(int id)
        {
            var contact = await _contactRepository.GetById(id);
            ValidateIfContactIsNull(id, contact);
            var apiResponse = new ApiResponse<ContactDto>();
            var contactDtoList = new List<ContactDto>();
            contactDtoList.Add(_mapper.Map<ContactDto>(contact));
            apiResponse.result = new ResultBody<ContactDto> { items = contactDtoList, totalAcount = 1 };
            return apiResponse;
        }

        public async Task<ApiResponse<ContactDto>> Create(CreateContactDto model)
        {
            await _createContactValidator.ValidateAndThrowAsync(model);
            var contact = await _contactRepository.Create(_mapper.Map<Contact>(model));
            var apiResponse = new ApiResponse<ContactDto>();
            var contactDtoList = new List<ContactDto>();
            contactDtoList.Add(_mapper.Map<ContactDto>(contact));
            apiResponse.result = new ResultBody<ContactDto> { items = contactDtoList, totalAcount = 1 };
            return apiResponse;
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var contact = await _contactRepository.GetById(id);
            ValidateIfContactIsNull(id, contact);
            contact.IsDeleted = true;
            var result = await _contactRepository.Delete(contact);
            var apiResponse = new ApiResponse<bool> { Success = result};
            return apiResponse;
        }

        public async Task<ApiResponse<ContactDto>> Update(int id, UpdateContactDto model)
        {
            if (id != model.Id) throw new BadRequestException("The query Id does not match with the Id provided in the body request");
            var contact = Task.Run(async () => await _contactRepository.GetById(id)).Result;
            ValidateIfContactIsNull(id, contact);
            var contactUpdated = await _contactRepository.Update(_mapper.Map(model, contact));
            var apiResponse = new ApiResponse<ContactDto>();
            var contactDtoList = new List<ContactDto>();
            contactDtoList.Add(_mapper.Map<ContactDto>(contactUpdated));
            apiResponse.result = new ResultBody<ContactDto> { items = contactDtoList, totalAcount = 1 };
            return apiResponse;
        }

        private void ValidateIfContactIsNull(int id, Contact contact)
        {
            if (contact == null) throw new NotFoundException($"No contact was found with id {id}");
        }
    }
}
