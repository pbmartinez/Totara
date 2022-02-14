using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class EscuelaDto: Entity
    {

        public EscuelaDto()
        {
            Estudiantes = new List<EstudianteDto>();
            Cursos = new List<CursoDto>();
        }

        public string Nombre { get; set; }


        //Navigation Properties
        public virtual List<EstudianteDto> Estudiantes { get; set; }
        public virtual List<CursoDto> Cursos { get; set; }
    }
}
