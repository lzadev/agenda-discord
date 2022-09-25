using Agenda.Context;
using Agenda.DTOs;
using Agenda.Filters;
using Agenda.Repository.Abstract;
using Agenda.Repository.Concret;
using Agenda.Services.Abstract;
using Agenda.Services.Concret;
using Agenda.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Agenda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateContactDto>, CreateContactValidator>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //
            services.AddTransient<IContactService, ContactService>();
            //
            services.AddTransient<IContactRepository, ContactRepository>();

            services.AddDbContext<AgendaContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("db")));
            services.AddControllers(opt =>
            {
                opt.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson();
        //    return builder
        //.GetRequiredService<IOptions<MvcOptions>>()
        //.Value
        //.InputFormatters
        //.OfType<NewtonsoftJsonPatchInputFormatter>()
        //.First();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
