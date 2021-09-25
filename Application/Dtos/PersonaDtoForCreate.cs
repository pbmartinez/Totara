using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class PersonaDtoForCreate
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
    }
}
