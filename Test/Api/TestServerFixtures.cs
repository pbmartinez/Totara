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
    class TestServerFixtures 
    {
        public WebApplicationFactory<Program> Application { get; set; }  

        private readonly TestServer _testServer ;
        public HttpClient HttpClient { get; }
        public IConfiguration Configuration => _testServer.Services.GetService<IConfiguration>()!;
        

        public TestServerFixtures()
        {
            Application = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
            });
            _testServer = Application.Server;
            
            HttpClient = Application.CreateClient();
        }

        private static Checkpoint CheckPoint = new()
        {
            TablesToIgnore = new Table[] { new Table("__EFMigrationsHistory") },
            DbAdapter = DbAdapter.MySql
        };

        public string BaseUrl => Configuration["Testing:BaseUrl"];

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = _testServer.Services.GetService<IServiceScopeFactory>()!.CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        public async Task ExecuteDbContextAsync(Func<UnitOfWorkContainer, Task> action)
        {
            await ExecuteScopeAsync(sp => action(sp.GetService<UnitOfWorkContainer>()!));
        }

        public async Task ResetDatabase()
        {
            var configuration = _testServer.Services.GetService<IConfiguration>();
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
