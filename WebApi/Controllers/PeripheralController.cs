using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/peripheral")]
    [ApiController]
    public class PeripheralController : ApiBaseController<PeripheralDto, PeripheralDtoForCreate, PeripheralDtoForUpdate>
    {
        public PeripheralController(IPeripheralAppService appService, ILogger<ApiBaseController<PeripheralDto, PeripheralDtoForCreate, PeripheralDtoForUpdate>> logger)
            : base(appService, logger)
        {
        }
    }
}
