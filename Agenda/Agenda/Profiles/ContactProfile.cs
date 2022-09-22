using Agenda.DTOs;
using Agenda.Models;
using AutoMapper;

namespace Agenda.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<CreateContactDto, Contact>();
            CreateMap<UpdateContactDto, Contact>();
        }
    }
}
