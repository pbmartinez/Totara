using Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public partial class MatriculaDtoForCreate : Entity
    {
        [Key]
        public virtual Guid EstudianteId { get; set; }
        [Key]
        public virtual Guid CursoId { get; set; }

        public double NotaFinal { get; set; }
    }
}
