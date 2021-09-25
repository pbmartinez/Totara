using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Estudiante : Entity
    {
        public Estudiante()
        {
            Matriculas = new List<Matricula>();
        }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        public int Grado { get; set; }

        // Navigation Properties
        [Required]
        public virtual Guid EscuelaId { get; set; }
        public virtual Escuela Escuela { get; set; }

        public virtual List<Matricula> Matriculas { get; set; }
    }
}
