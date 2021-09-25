using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class PersonaDto : Entity
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellidos}";
    }
}
