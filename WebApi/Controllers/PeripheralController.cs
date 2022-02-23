using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/peripheral")]
    public class PeripheralController : ApiBaseController<PeripheralDto>
    {
        private readonly IGatewayAppService _gatewayAppService;
        public PeripheralController(IGatewayAppService gatewayAppService, IPeripheralAppService appService, ILogger<ApiBaseController<PeripheralDto>> logger)
            : base(appService, logger)
        {
            _gatewayAppService = gatewayAppService;
            Includes = new List<Expression<Func<PeripheralDto, object>>>() { a => a.Gateway };
        }

        [HttpGet]
        [HttpHead]
        [Route("~/api/gateway/{gatewayId}/peripheral/{peripheralId}")]
        public async Task<ActionResult<PeripheralDto>> GetPeripheralForGateway(Guid? gatewayId, Guid? peripheralId)
        {
            if (gatewayId == null || gatewayId == Guid.Empty)
                return BadRequest();
            if (peripheralId == null || peripheralId == Guid.Empty)
                return BadRequest();
            var gateway = await _gatewayAppService.GetAsync(gatewayId.Value, new List<Expression<Func<GatewayDto, object>>> { g=>g.Peripherals});
            if (gateway == null)
                return NotFound();
            var peripheral = gateway.Peripherals.FirstOrDefault(p=> p.Id == peripheralId.Value);
            if (peripheral == null)
                return NotFound();
            return Ok(peripheral);
        }

        [HttpPost]
        [Route("~/api/gateway/{gatewayId}/peripheral")]
        public async Task<ActionResult> PostPeripheralForGateway(Guid? gatewayId, PeripheralDto peripheral)
        {
            if (gatewayId == null || gatewayId == Guid.Empty)
                return BadRequest();
            var gateway = await _gatewayAppService.GetAsync(gatewayId.Value);
            if(gateway == null)
                return BadRequest();
            peripheral.Id = Guid.NewGuid();
            peripheral.GatewayId = gatewayId.Value;
            var result = await AppService.AddAsync(peripheral);
            var peripheralDto = await AppService.GetAsync(peripheral.Id);
            return CreatedAtAction(nameof(GetPeripheralForGateway), new { gatewayId = gatewayId.Value, peripheralId = peripheral.Id }, peripheralDto);
        }

    }
}
