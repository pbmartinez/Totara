using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Matricula:Entity
    {
        [ForeignKey("Estudiante")]
        [Required]
        public virtual Guid EstudianteId { get; set; }

        [ForeignKey("Curso")]
        [Required]
        public virtual Guid CursoId { get; set; }

        public virtual Estudiante Estudiante { get; set; }
        public virtual Curso Curso { get; set; }


        public double NotaFinal { get; set; }
    }
}
