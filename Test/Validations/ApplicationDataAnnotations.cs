using Application.DataAnnotations;
using AutoMapper;
using FluentAssertions;
using Infraestructure.Application.AppServices;
using Infraestructure.Application.Validator;
using Infraestructure.Domain.Repositories;
using Infraestructure.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Validations
{
    [TestFixture]
    [Parallelizable]
    public class ApplicationDataAnnotations
    {
        [Test]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("399.168.10.1", false)]
        [TestCase("192.399.10.1", false)]
        [TestCase("192.168.399.1", false)]
        [TestCase("192.168.10.399", false)]
        [TestCase("192.168.10.1", true)]
        public void ValidatingIpAddressAttribute(string ipAddress, bool expectedResult)
        {
            var ipv4AddressAttribute = new Ipv4Address();

            var validationResult = ipv4AddressAttribute.IsValid(ipAddress);
            validationResult.Should().Be(expectedResult);
        }
    }
}
