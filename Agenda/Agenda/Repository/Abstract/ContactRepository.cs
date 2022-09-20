﻿using Agenda.Context;
using Agenda.Models;
using Agenda.Repository.Concret;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Repository.Abstract
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaContext context;
        public ContactRepository(AgendaContext context)
        {
            this.context = context;
        }

        public async Task<Contact> Create(Contact model)
        {
            await context.Contacts.AddAsync(model);
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(Contact model)
        {
            context.Contacts.Update(model);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetById(int id)
        {
            return await context.Contacts.FindAsync(id);
        }

        public async Task<Contact> Update(Contact model)
        {
            context.Contacts.Update(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}
