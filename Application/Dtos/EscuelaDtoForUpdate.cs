using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class EscuelaDtoForUpdate: Entity
    {
        public string Nombre { get; set; }
    }
}
