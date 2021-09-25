using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Escuela: Entity
    {

        public Escuela()
        {
            Estudiantes = new List<Estudiante>();
            Cursos = new List<Curso>();
        }

        public string Nombre { get; set; }


        //Navigation Properties
        public virtual List<Estudiante> Estudiantes { get; set; }
        public virtual List<Curso> Cursos { get; set; }
    }
}
