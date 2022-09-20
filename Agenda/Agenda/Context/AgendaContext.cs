using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Context
{
    public class AgendaContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
