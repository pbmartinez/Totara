using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : ApiBaseController<GatewayDto, GatewayDtoForCreate, GatewayDtoForUpdate>
    {
        public GatewayController(IGatewayAppService appService, ILogger<ApiBaseController<GatewayDto, GatewayDtoForCreate, GatewayDtoForUpdate>> logger)
            : base(appService, logger)
        {
        }
    }
}
