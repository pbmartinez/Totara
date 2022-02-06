using Application.AppServices;
using Application.IAppServices;
using Application.Mappings;
using AutoMapper.Extensions.ExpressionMapping;
using Domain.IRepositories;
using Domain.Repositories;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
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
            services.AddControllersWithViews();

            services.AddAutoMapper(configuration =>
            {
                configuration.AddExpressionMapping();
                configuration.AddProfile<CasaProfile>();
                configuration.AddProfile<CursoProfile>();
                configuration.AddProfile<EscuelaProfile>();
                configuration.AddProfile<EstudianteProfile>();
                configuration.AddProfile<MatriculaProfile>();
                configuration.AddProfile<PersonaProfile>();
                AppDomain.CurrentDomain.GetAssemblies();
                
            });

            
            /*Why scoped ?
             * Lifetime of Unit of Work should be: 
             * 1. long enough to contain all changes in a single block of transacctions (tipically a request), and
             * 2. short enough to avoid interfere with other changes carried out in another block of transacctions
             * AddSingleton -> Application Lifetime
             * AddScoped -> Request Lifetime
             * AddTransient -> Every time is invoked.
             * Unit of Work should be Scoped. And because is injected into Repositories these ones should be scoped as well.
             * When scoped services injected into singleton services their life time is altered and become singleton.
             * Also .Net's build in dependency injection, validates such a rule, throwing an exception.
             */
            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWorkContainer>();            
            //AppServices
            services.AddScoped<ICasaAppService, CasaAppService>();
            services.AddScoped<IPersonaAppService, PersonaAppService>();
            services.AddScoped<ICursoAppService, CursoAppService>();
            services.AddScoped<IEscuelaAppService, EscuelaAppService>();
            services.AddScoped<IEstudianteAppService, EstudianteAppService>();
            services.AddScoped<IMatriculaAppService, MatriculaAppService>();
            //Repositories
            services.AddScoped<ICasaRepository, CasaRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<IEscuelaRepository, EscuelaRepository>();
            services.AddScoped<IEstudianteRepository, EstudianteRepository>();
            services.AddScoped<IMatriculaRepository, MatriculaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
