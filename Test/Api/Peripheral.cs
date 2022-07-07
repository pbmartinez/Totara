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
    public class Peripheral
    {
        private static WebApiApplication Application { get; set; } = WebApiApplication.GetWebApiApplication();
        private HttpClient HttpClient { get; set; } = Application.CreateClient();


        [Test]
        public async Task Get_Peripherals()
        {
            var response = await Application.Server.CreateHttpApiRequest<PeripheralController>(g => g.Get(new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Test]
        [TestCase("10887BC2-B061-43F3-0000-913548EEC829", HttpStatusCode.NotFound)]
        public async Task Get_Peripheral_by_id(Guid id, HttpStatusCode statusCode)
        {
            var response = await Application.Server.CreateHttpApiRequest<PeripheralController>(g => g.Get(id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            response.StatusCode.Should().Be(statusCode);
        }

        [Test]
        [TestCase("4D9E5E60-16F6-4789-B7E4-744A5E067109", true, "Nike", HttpStatusCode.Created)]
        [TestCase("4D9E5E60-16F6-4789-B7E4-744A5E067109", true, null, HttpStatusCode.BadRequest)]
        public async Task Post_Peripheral(Guid gatewayId, bool status, string vendor, HttpStatusCode statusCode)
        {
            var Peripheral = new PeripheralDto()
            {
                CreatedDate = DateTime.Now,
                GatewayId = gatewayId,
                Status = status,
                Vendor = vendor
            };
            var response = await Application.Server.CreateHttpApiRequest<PeripheralController>(g => g.Post(Peripheral, new CancellationToken()))
                .PostAsync();
            response.StatusCode.Should().Be(statusCode);
        }

        [Test]
        [TestCase("F3CEE0FF-14AC-47A5-875E-08D9F7B41187", "4D9E5E60-16F6-4789-B7E4-744A5E067109", true, null, HttpStatusCode.BadRequest)]
        [TestCase("F3CEE0FF-0000-0000-0000-08D9F7B41187", "4D9E5E60-16F6-4789-0000-744A5E067109", true, "Nike", HttpStatusCode.NotFound)]
        public async Task Put_Peripheral(Guid id,Guid gatewayId, bool status, string vendor, HttpStatusCode statusCode)
        {
            var Peripheral = new PeripheralDto()
            {
                CreatedDate = DateTime.Now,
                GatewayId = gatewayId,
                Status = status,
                Vendor = vendor
            };
            var response = await HttpClient.PutAsJsonAsync(ApiEndpoints.Put.Peripheral(id), Peripheral);
            response.StatusCode.Should().Be(statusCode);
        }

        [Test]
        [TestCase("10887BC2-B061-43F3-0000-913548EEC829", HttpStatusCode.NotFound)]
        public async Task Delete_Peripheral(Guid id, HttpStatusCode status)
        {
            var response = await Application.Server.CreateHttpApiRequest<PeripheralController>(g => g.Delete(id, new CancellationToken()))
                .SendAsync("Delete");
            response.StatusCode.Should().Be(status);
        }


    }
}
