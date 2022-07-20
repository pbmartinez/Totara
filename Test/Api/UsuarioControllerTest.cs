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
    public class UsuarioControllerTest
    {
        private static WebApiApplication Application { get; set; } = WebApiApplication.GetWebApiApplication();
        private HttpClient HttpClient { get; set; } = Application.CreateClient();

        #region Data Set


        private static readonly object[] UsuariosWithResultsOnPost =
        {
            new object[]{   new UsuarioDto{Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.Created },
            new object[]{   new UsuarioDto{Nombre = "", Username="krystina",Email="eleazar1988@hotmail.com",Suspended= false } ,
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Nombre = "Rose S Henson", Username="krystina",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Nombre = "Rose S Henson", Username="krystina",Email="",Suspended= false},
                            HttpStatusCode.BadRequest }
        };
        private static readonly object[] UsuariosWithResultsOnPut =
        {
            new object[]{   new UsuarioDto{Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.NoContent },
            new object[]{   new UsuarioDto{Nombre = "", Username="krystina",Email="eleazar1988@hotmail.com",Suspended= false } ,
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Nombre = "Rose S Henson", Username="krystina",Email="braulio.muell@hotmail.com",Suspended= false},
                            HttpStatusCode.BadRequest },
            new object[]{   new UsuarioDto{Nombre = "Rose S Henson", Username="krystina",Email="",Suspended= false},
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
        public async Task Get_Usuario_by_id(int id, HttpStatusCode statusCode)
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Get(id, new QueryStringParameters(), new CancellationToken()))
                .GetAsync();
            response.StatusCode.Should().Be(statusCode);
        }

        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnPost))]
        public async Task Post_Usuario(UsuarioDto item, HttpStatusCode status)
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Post(item, new CancellationToken()))
                .PostAsync();
            response.StatusCode.Should().Be(status);
        }

        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnPut))]
        public async Task Put_Usuario(UsuarioDto item, HttpStatusCode status)
        {
            var response = await HttpClient.PutAsJsonAsync(ApiEndpoints.Put.Usuario(item.Id), item);
            response.StatusCode.Should().Be(status);
        }

        [Test]
        [TestCaseSource(nameof(UsuariosWithResultsOnDelete))]
        public async Task Delete_Usuario(int id, HttpStatusCode status)
        {
            var response = await Application.Server.CreateHttpApiRequest<UsuarioController>(g => g.Delete(id, new CancellationToken()))
                .SendAsync("Delete");
            response.StatusCode.Should().Be(status);
        }

    }
}
