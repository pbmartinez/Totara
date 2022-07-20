using Application.Dtos;
using Application.IAppServices;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ApiBaseController<UsuarioDto, int>
    {
        public UsuarioController(IUsuarioAppService appService, ILogger<ApiBaseController<UsuarioDto, int>> logger, IPropertyCheckerService propertyCheckerService) : base(appService, logger, propertyCheckerService)
        {
        }
    }
}
