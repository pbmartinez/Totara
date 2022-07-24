using Domain.Entities;
using Infrastructure.Domain.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
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
            DbAdapter = DbAdapter.MySql
        };
        private WebApiApplication()
        {
            
        }

        public static WebApiApplication GetWebApiApplication()
        {
            if (_webApiApplication == null)
            {
                _webApiApplication = new WebApiApplication();
                _webApiApplication.WithWebHostBuilder(builder => {
                    builder.UseEnvironment("Testing");
                });
                
            }
            return _webApiApplication;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
        }
        public IConfiguration Configuration => Services.GetService<IConfiguration>()!;

        public string BaseUrl => Configuration["Testing:BaseUrl"];

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
            using var conn = new MySqlConnection(connectionString ?? throw new ArgumentNullException(nameof(connectionString)));
            await conn.OpenAsync();
            await CheckPoint.Reset(conn);
        }

        public async Task<Usuario> AnUserInTheDatabase()
        {
            var item = new Usuario()
            {
                Id = 1,
                Nombre = "Jeffery K. Hope",
                Username = "Lizinars",
                Email = "JefferyKHope@jourrapide.com",
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
