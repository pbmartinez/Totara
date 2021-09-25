using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Dtos
{
    public class CursoDto : Entity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        //Navigation Properties
        public virtual Guid EscuelaId { get; set; }
        public virtual EscuelaDto Escuela { get; set; }

        public virtual List<MatriculaDto> Matriculas { get; set; }
    }
}
