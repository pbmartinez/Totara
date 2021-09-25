using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class EstudianteController : BaseController<EstudianteDto, EstudianteDtoForCreate, EstudianteDtoForUpdate>
    {
        private readonly IEscuelaAppService _escuelaAppService;
        public EstudianteController(IEstudianteAppService EstudianteAppService,
            IEscuelaAppService escuelaAppService) : base(EstudianteAppService)
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
