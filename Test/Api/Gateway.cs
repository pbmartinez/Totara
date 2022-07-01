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
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Test.Api
{
    [TestFixture]
    public class Gateway
    {
        private static WebApiApplication Application { get; set; } = new WebApiApplication();
        private HttpClient HttpClient { get; set; } = Application.CreateClient();


        [Test]
        public async Task get_gateways()
        {
            var response = await HttpClient.GetStringAsync(ApiEndpoints.Get.Gateway());
            var respons = await HttpClient.GetAsync(ApiEndpoints.Get.Gateway());
        }
        
        
        
        [Test]
        [TestCase("10887BC2-B061-43F3-0000-913548EEC829", 404)]
        public async Task get_gateway_by_id(Guid id, int statusCodes)
        {
            var respons = await HttpClient.GetAsync(ApiEndpoints.Get.Gateway(id));
            var responseCode = (int)respons.StatusCode;
            responseCode.Should().Be(statusCodes);
        }

        [Test]
        public async Task post_gateway()
        {
            var gat = new GatewayDto()
            {
                Name = "Gatew",
                Ipv4Address = "125.125.125.36"
            };
            var respons = await HttpClient.PostAsJsonAsync<GatewayDto>(ApiEndpoints.Post.Gateway(),gat);
            var responseCode = (int)respons.StatusCode;
            responseCode.Should().Be(201);
        }
    }
}
