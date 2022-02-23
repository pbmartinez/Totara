using Application.Dtos;
using Application.Exceptions;
using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Infraestructure.Application.AppServices;
using Infraestructure.Application.Validator;
using Infraestructure.Domain.Repositories;
using Infraestructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.AppServices
{
    [TestFixture]
    public class AppServices
    {
        private static Guid gatewayId = Guid.NewGuid();

        private static readonly object[] GatewaysWithValidationFails =
        {            
            new GatewayDto{Id = new Guid("c48c2a37-4388-4fa0-96d8-f97a61cbef74"),Name="Name 1",Ipv4Address="4999.168.10.1" },            
            new GatewayDto{Id = new Guid("e36bfda8-54c8-4807-8111-31e7efc8ac18"),Name="Name 1",Ipv4Address="192.4999.10.1" },
            new GatewayDto{Id = new Guid("a0c892c8-0bcd-472b-9380-f9a972f65d15"),Name="Name 1",Ipv4Address="192.168.10.1",
                Peripherals=new List<PeripheralDto>() {
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
                new PeripheralDto{ Id = Guid.NewGuid(), Vendor="Pa & Co",CreatedDate=DateTime.Today,Status=true, GatewayId=gatewayId},
            } }
        };

        [Test]
        [TestCaseSource(nameof(GatewaysWithValidationFails))]
        public void AddGatewaysThrowExceptionOnValidationFails(GatewayDto item)
        {
            var options = DataBaseService.DataBaseProviderSetUp.SQLServerInMemoryOptions("database");
            var context = new UnitOfWorkContainer(options);
            var gatewayRepository = new GatewayRepository(context);
            var mapper = new Mock<IMapper>();
            var gatewayService = new GatewayAppService(gatewayRepository, mapper.Object, new DataAnnotationsEntityValidator());

            Action action = () => gatewayService.AddAsync(item).Wait();

            //Action action = async () => await gatewayService.AddAsync(item);
            //Tomar como referencia para arreglar el otro metodo

            //Func<GatewayDto, Task<bool>> action = async (x) => await gatewayService.AddAsync(x);
            using (new AssertionScope())
            {
                //action.Should().Throw<NullReferenceException>();
                //action.Should().Throw<ArgumentNullException>();
                action.Should().Throw<ApplicationValidationErrorsException>();
            }
        }

        [Test]
        
        public async Task Testing()
        {
            var options = DataBaseService.DataBaseProviderSetUp.SQLServerInMemoryOptions("database");
            var context = new UnitOfWorkContainer(options);
            var gatewayRepository = new GatewayRepository(context);
            var mapper = new Mock<IMapper>();
            var gatewayService = new GatewayAppService(gatewayRepository, mapper.Object, new DataAnnotationsEntityValidator());

            var a = await gatewayService.GetAllAsync(null,null);
            var dd = await gatewayService.AddAsync((GatewayDto)GatewaysWithValidationFails[0]);
            
        }
    }
}
