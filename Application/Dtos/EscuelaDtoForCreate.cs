using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class EscuelaDtoForCreate: Entity
    {
        public string Nombre { get; set; }
    }
}
