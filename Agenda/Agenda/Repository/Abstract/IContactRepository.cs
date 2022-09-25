using Agenda.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Repository.Abstract
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<IEnumerable<Contact>> GetAll(Func<Contact, bool> func);
        Task<Contact> GetById(int id);
        Task<Contact> Create(Contact model);
        Task<Contact> Update(Contact model);
        Task<bool> Delete(Contact model);
    }
}
