using Application.Dtos;
using FluentAssertions;
using FluentAssertions.Execution;
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
    public class UsuarioControllerTest
    {
        private static WebApiApplication Application { get; set; } = WebApiApplication.GetWebApiApplication();
        private HttpClient HttpClient { get; set; } = Application.CreateClient();

        #region Data Set


        private static readonly object[] UsuariosWithResultsOnPost =
        {
            new object[]{   new UsuarioDto{Id = 2, Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.Created },
            new object[]{   new UsuarioDto{Id = 3, Nombre = "", Username="krystina",Email="eleazar1988@hotmail.com",Suspended= false } ,
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Id = 4, Nombre = "Rose S Henson", Username="",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Id = 5, Nombre = "Rose S Henson", Username="krystina",Email="",Suspended= false},
                            HttpStatusCode.BadRequest }
        };
        private static readonly object[] UsuariosWithResultsOnPut =
        {
            new object[]{   new UsuarioDto{Id = 1, Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.NoContent },
            new object[]{   new UsuarioDto{Id = 1, Nombre = "", Username="krystina",Email="eleazar1988@hotmail.com",Suspended= false } ,
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Id = 1, Nombre = "Rose S Henson", Username="",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Id = 1, Nombre = "Rose S Henson", Username="krystina",Email="",Suspended= false},
                            HttpStatusCode.BadRequest }
        };

        private static readonly object[] UsuariosWithResultsOnGet =
        {
            new object[]{ 1, HttpStatusCode.OK },
            new object[]{ 33, HttpStatusCode.NotFound }
        };

        private static readonly object[] UsuariosWithResultsOnDelete =
        {
            new object[]{ 1, HttpStatusCode.NoContent },
            new object[]{ 33, HttpStatusCode.NotFound }
        };

        #endregion


        [SetUp]
        public async Task ResetBeforeTest()
        {
            await WebApiApplication.ResetDatabase();
            await WebApiApplication.GetWebApiApplication().AnUserInTheDatabase();
        }
        

        [Test]
        public async Task Get_Usuarios()
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Get(new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnGet))]
        public async Task Get_Usuario_By_Id(int id, HttpStatusCode statusCode)
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Get(id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();

            response.StatusCode.Should().Be(statusCode);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UsuarioDto>();
                result.Should().NotBeNull();
                result!.Id.Should().Be(id);
            }

        }

        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnPost))]
        public async Task Post_Usuario(UsuarioDto item, HttpStatusCode status)
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Post(item, new CancellationToken()))
                .PostAsync();
            var responseAfterPost = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Get(item.Id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(status);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    responseAfterPost.StatusCode.Should().Be(HttpStatusCode.NotFound);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    responseAfterPost.StatusCode.Should().Be(HttpStatusCode.OK);
                    var result = await responseAfterPost.Content.ReadFromJsonAsync<UsuarioDto>();
                    responseAfterPost.Content.Should().NotBeNull();
                    result.Should().BeEquivalentTo(item);
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnPut))]
        public async Task Put_Usuario(UsuarioDto item, HttpStatusCode status)
        {
            var response = await HttpClient.PutAsJsonAsync(Application.BaseUrl + ApiEndpoints.Put.Usuario(item.Id), item);
            var responseAfterPut = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Get(item.Id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(status);
                if (response.StatusCode == HttpStatusCode.NotFound)
                    responseAfterPut.StatusCode.Should().Be(HttpStatusCode.NotFound);
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    responseAfterPut.StatusCode.Should().Be(HttpStatusCode.OK);
                    var result = await responseAfterPut.Content.ReadFromJsonAsync<UsuarioDto>();
                    result.Should().BeEquivalentTo(item);
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnDelete))]
        public async Task Delete_Usuario(int id, HttpStatusCode status)
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Delete(id, new CancellationToken()))
                .SendAsync("Delete");
            var responseAfterDelete = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Get(id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(status);
                responseAfterDelete.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

    }
}
