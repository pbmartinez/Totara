using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            //IncludesList = new() { a => a.Escuela, a => a.Matriculas };
            //IncludesList = new() { a => a.Escuela, a => a.Matriculas, a => a.Matriculas.Select(b => b.Curso) };
            //IncludesList = new() { a => a.Escuela, a => a.Matriculas.Select(b =>  b.Curso) };
            Includes = null;
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
