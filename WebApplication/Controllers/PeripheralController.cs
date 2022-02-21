using Application.Dtos;
using Application.IAppServices;
using Application.Specification;
using AutoMapper;
using Domain.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class PeripheralController : BaseController<PeripheralDto, PeripheralDtoForCreate, PeripheralDtoForUpdate>
    {
        public PeripheralController(IPeripheralAppService PeripheralAppService) : base(PeripheralAppService)
        {

        }
        
    }
}
