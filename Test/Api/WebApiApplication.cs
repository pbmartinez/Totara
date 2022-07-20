using Domain.Entities;
using Infrastructure.Domain.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Respawn;
using Respawn.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api
{
    class WebApiApplication : WebApplicationFactory<Program>
    {
        private static WebApiApplication _webApiApplication = null!;
        private static Checkpoint CheckPoint = new Checkpoint()
        {
            TablesToIgnore = new Table[] { new Table("__EFMigrationsHistory") },
            
        };
        private WebApiApplication()
        {

        }

        public static WebApiApplication GetWebApiApplication()
        {
            if (_webApiApplication == null)
            {
                _webApiApplication = new WebApiApplication();
            }
            return _webApiApplication;
        }


        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = GetWebApiApplication().Services.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        public async Task ExecuteDbContextAsync(Func<UnitOfWorkContainer, Task> action)
        {
            await ExecuteScopeAsync(sp => action(sp.GetService<UnitOfWorkContainer>()!));
        }

        public static async Task ResetDatabase()
        {
            var configuration = GetWebApiApplication().Services.GetService<IConfiguration>();
            var connectionString = configuration?["ConnectionStrings:DefaultConnection"];
            await CheckPoint.Reset("Server=localhost;Database=TotaraTest;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        public async Task<Usuario> AnUserInTheDatabase()
        {
            var item = new Usuario()
            {
                Id = 1,
                Nombre = "",
                Username = "",
                Email = "",
                Suspended = false
            };

            await ExecuteDbContextAsync(async context =>
            {
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            });
            return item;
        }

    }
}
