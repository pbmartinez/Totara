using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Persona : Entity
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
    }
}
