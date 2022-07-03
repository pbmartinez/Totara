using Application.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Parameters;

namespace Test.Api
{
    [TestFixture]
    public class Gateway
    {
        private static WebApiApplication Application { get; set; } = WebApiApplication.GetWebApiApplication();
        private HttpClient HttpClient { get; set; } = Application.CreateClient();


        [Test]
        public async Task Get_gateways()
        {
            var response = await Application.Server.CreateHttpApiRequest<GatewayController>(g => g.Get(new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Test]
        [TestCase("10887BC2-B061-43F3-0000-913548EEC829", HttpStatusCode.NotFound)]
        public async Task Get_gateway_by_id(Guid id, HttpStatusCode statusCode)
        {
            var response = await Application.Server.CreateHttpApiRequest<GatewayController>(g => g.Get(id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            response.StatusCode.Should().Be(statusCode);
        }

        [Test]
        [TestCase("Gatew", "125.125.125.36", HttpStatusCode.Created)]
        [TestCase(null, null, HttpStatusCode.BadRequest)]
        [TestCase(null, "125.125.125.36", HttpStatusCode.BadRequest)]
        [TestCase("Gatew", null, HttpStatusCode.BadRequest)]
        [TestCase("Gatew", "300.2014.33.6", HttpStatusCode.BadRequest)]
        public async Task Post_gateway(string name, string ip, HttpStatusCode status)
        {
            var gateway = new GatewayDto()
            {
                Name = name,
                Ipv4Address = ip
            };
            var response = await Application.Server.CreateHttpApiRequest<GatewayController>(g => g.Post(gateway, new CancellationToken()))
                .PostAsync();
            response.StatusCode.Should().Be(status);
        }

        [Test]
        [TestCase("10887BC2-B061-43F3-0000-913548EEC829", "Gatew", "125.125.125.36", HttpStatusCode.NotFound)]
        public async Task Put_gateway(Guid id, string name, string ip, HttpStatusCode status)
        {
            var gateway = new GatewayDto()
            {
                Id = id,
                Name = name,
                Ipv4Address = ip
            };            
            var response = await HttpClient.PutAsJsonAsync(ApiEndpoints.Put.Gateway(id), gateway);
            response.StatusCode.Should().Be(status);
        }

        [Test]
        [TestCase("10887BC2-B061-43F3-0000-913548EEC829", HttpStatusCode.NotFound)]
        public async Task Delete_gateway(Guid id, HttpStatusCode status)
        {
            var response = await Application.Server.CreateHttpApiRequest<GatewayController>(g => g.Delete(id, new CancellationToken()))
                .SendAsync("Delete");
            response.StatusCode.Should().Be(status);
        }


    }
}
