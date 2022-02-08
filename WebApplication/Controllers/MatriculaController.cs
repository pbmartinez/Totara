using Application.Dtos;
using Application.IAppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class MatriculaController : BaseController<MatriculaDto, MatriculaDtoForCreate, MatriculaDtoForUpdate>
    {
        private readonly ICursoAppService _cursoAppService;
        private readonly IEstudianteAppService _estudianteAppService;
        public MatriculaController(IMatriculaAppService MatriculaAppService,
            ICursoAppService cursoAppService,
            IEstudianteAppService estudianteAppService) : base(MatriculaAppService)
        {
            _cursoAppService = cursoAppService;
            _estudianteAppService = estudianteAppService;

            Includes = new() { a => a.Curso, a => a.Estudiante };
            DetailsIncludes = new() { a => a.Curso, a => a.Estudiante };
            DeleteIncludes = new() { a => a.Curso, a => a.Estudiante };
            EditIncludes = new() { a => a.Curso, a => a.Estudiante };
        }
        public override async Task CargarViewBagsCreate()
        {
            var cursos = await _cursoAppService.GetAllAsync();
            ViewBag.Cursos = new SelectList(cursos, "Id", "Nombre");

            var estudiantes = await _estudianteAppService.GetAllAsync();
            ViewBag.Estudiantes = new SelectList(estudiantes, "Id", "Nombre");
        }
        public override async Task CargarViewBagsEdit(Guid id)
        {
            var cursos = await _cursoAppService.GetAllAsync();
            ViewBag.Cursos = new SelectList(cursos, "Id", "Nombre");

            var estudiantes = await _estudianteAppService.GetAllAsync();
            ViewBag.Estudiantes = new SelectList(estudiantes, "Id", "Nombre");
        }

    }
}
