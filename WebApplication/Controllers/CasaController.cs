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
    public class CasaController : BaseController<CasaDto, CasaDtoForCreate, CasaDtoForUpdate>
    {
        private readonly IMapper _mapper;
        public CasaController(ICasaAppService CasaAppService, IMapper mapper) : base(CasaAppService)
        {
            _mapper = mapper;
        }
        public override async Task<IActionResult> Index()
        {
            var specifi = new CasaMas2CuartosSpecification(_mapper);
            //specifi.
            var items = await AppService.FindAllBySpecificationPatternAsync();
            return await Task.FromResult(View(items));
        }
    }
}
