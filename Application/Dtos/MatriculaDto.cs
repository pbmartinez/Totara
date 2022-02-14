using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public partial class MatriculaDto:Entity
    {
        [Key]
        public virtual Guid EstudianteId { get; set; }
        [Key]
        public virtual Guid CursoId { get; set; }

        public EstudianteDto Estudiante { get; set; }
        public Curso Curso { get; set; }


        public double NotaFinal { get; set; }
    }
}
