using Application.Dtos;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Infrastructure.Application.AppServices;
using Infrastructure.Application.Validator;
using Infrastructure.Domain.Repositories;
using Infrastructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Test.AppServices
{
    [TestFixture]
    public class AppServices
    {


        #region Data Set


        private static readonly object[] UsuariosWithValidationFails =
        {
            new object[]{   new UsuarioDto{Id = 1, Nombre = "", Username="krystina",Email="eleazar1988@hotmail.com",Suspended= false } ,
                            new Usuario{Id = 1, Nombre = "James A Gaines", Username="krystina",Email="",Suspended= false}
            },
            new object[]{   new UsuarioDto{Id = 1, Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false},
                            new Usuario{Id = 1, Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false} }

        };

        private static readonly object[] UsuariosWithValidationSuccess =
        {
            new Usuario{Id = 1, Nombre = "James A Gaines", Username="krystina",Email="eleazar1988@hotmail.com",Suspended= false},
            new Usuario{Id = 1, Nombre = "Rose S Henson", Username="Ieyahthoov4",Email="braulio.muell@hotmail.com",Suspended= false}
        };
        #endregion



        [Test]
        [TestCaseSource(nameof(UsuariosWithValidationFails))]
        public void AddUsuariosThrowExceptionOnValidationFails(UsuarioDto itemDtoVersion, Usuario entityVersion)
        {
            var options = DataBaseService.DataBaseProviderSetUp.SQLServerInMemoryOptions("database");
            var context = new UnitOfWorkContainer(options);
            var UsuarioRepository = new UsuarioRepository(context);
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<Usuario>(itemDtoVersion)).Returns(entityVersion);
            var UsuarioService = new UsuarioAppService(UsuarioRepository, mockedMapper.Object, new DataAnnotationsEntityValidator());

            Action action = () => UsuarioService.AddAsync(itemDtoVersion).Wait();

            action.Should().Throw<ApplicationValidationErrorsException>();
        }


        [Test]
        [TestCaseSource(nameof(UsuariosWithValidationSuccess))]
        public async Task AddUsuariosSuccesfullySaved(Usuario entityVersion)
        {
            var options = DataBaseService.DataBaseProviderSetUp.SQLServerInMemoryOptions(nameof(AddUsuariosSuccesfullySaved));
            var context = new UnitOfWorkContainer(options);
            var UsuarioRepository = new UsuarioRepository(context);

            await UsuarioRepository.AddAsync(entityVersion);
            await UsuarioRepository.UnitOfWork.CommitAsync();

            var g = await UsuarioRepository.GetAsync(entityVersion.Id);
            var gat = await UsuarioRepository.GetAllAsync(null);
            g.Should().NotBeNull();
        }
    }
}
