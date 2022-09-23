using Agenda.DTOs;
using System.Threading.Tasks;

namespace Agenda.Services.Abstract
{
    public interface IContactService
    {
        Task<ApiResponse<ContactDto>> GetAll();
        Task<ApiResponse<ContactDto>> GetById(int id);
        Task<ApiResponse<ContactDto>> Create(CreateContactDto model);
        Task<ApiResponse<ContactDto>> Update(int id, UpdateContactDto model);
        Task<ApiResponse<bool>> Delete(int id);
    }
}
