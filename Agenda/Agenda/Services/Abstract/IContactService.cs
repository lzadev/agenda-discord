using Agenda.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Services.Abstract
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAll();
        Task<ContactDto> GetById(int id);
        Task<ContactDto> Create(CreateContactDto model);
        Task<ContactDto> Update(int id, UpdateContactDto model);
        void Delete(int id);
    }
}
