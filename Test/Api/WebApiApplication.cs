using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
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


        public override IServiceProvider Services => base.Services;

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // shared extra set up goes here
            return base.CreateHost(builder);
        }

        protected override IHostBuilder? CreateHostBuilder()
        {
            return base.CreateHostBuilder();
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            return base.CreateServer(builder);
        }

        protected override IWebHostBuilder? CreateWebHostBuilder()
        {
            return base.CreateWebHostBuilder();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override IEnumerable<Assembly> GetTestAssemblies()
        {
            return base.GetTestAssemblies();
        }
    }
}
