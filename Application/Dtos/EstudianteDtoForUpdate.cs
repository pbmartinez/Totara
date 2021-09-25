using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class EstudianteDtoForUpdate : Entity
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        public int Grado { get; set; }

        // Navigation Properties
        [Required]
        public virtual Guid EscuelaId { get; set; }

    }
}
