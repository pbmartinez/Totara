using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class CasaDtoForCreate: Entity
    {
        public string Color { get; set; }
        public int CantidadCuartos { get; set; }
    }
}
