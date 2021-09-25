using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class CursoController : BaseController<CursoDto, CursoDtoForCreate, CursoDtoForUpdate>
    {
        private readonly IEscuelaAppService _escuelaAppService;
        public CursoController(ICursoAppService cursoAppService,
            IEscuelaAppService escuelaAppService) : base(cursoAppService)
        {
            _escuelaAppService = escuelaAppService;
        }
        public override async Task CargarViewBagsCreate()
        {
            var items = await _escuelaAppService.GetAllAsync();
            ViewBag.Escuelas = new SelectList(items, "Id", "Nombre");
        }
        public override async Task CargarViewBagsEdit(Guid id)
        {
            var items = await _escuelaAppService.GetAllAsync();
            ViewBag.Escuelas = new SelectList(items, "Id", "Nombre");
        }
    }
}
