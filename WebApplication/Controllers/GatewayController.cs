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
    public class GatewayController : BaseController<GatewayDto, GatewayDtoForCreate, GatewayDtoForUpdate>
    {
        public GatewayController(IGatewayAppService GatewayAppService) : base(GatewayAppService)
        {

        }
    }
}
