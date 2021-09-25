using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Casa:Entity
    {
        public string Color { get; set; }
        public int CantidadCuartos { get; set; }
    }
}
