using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public partial class CursoDtoForUpdate: Entity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


        //Navigation Properties
        [Required]
        public virtual Guid EscuelaId { get; set; }

    }
}
