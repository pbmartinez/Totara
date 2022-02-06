using Application.Dtos;
using Application.IAppServices;
using Application.Specification;
using Domain.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class CasaController : BaseController<CasaDto, CasaDtoForCreate, CasaDtoForUpdate>
    {
        public CasaController(ICasaAppService CasaAppService) : base(CasaAppService)
        {

        }
        public override async Task<IActionResult> Index()
        {
            var specifi = new CasaMas2CuartosSpecification();
            //specifi.
            var items = await AppService.FindWithSpecificationPatternAsync();
            return await Task.FromResult(View(items));
        }
    }
}
