using Domain.Entities;
using Domain.Entities.Base;
using Domain.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioDto : Entity
    {
        [Required(ErrorMessageResourceName = nameof(Resource.validation_FieldRequired),
                  ErrorMessageResourceType = typeof(Resource))]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessageResourceName = nameof(Resource.validation_FieldRequired),
                  ErrorMessageResourceType = typeof(Resource))]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessageResourceName = nameof(Resource.validation_FieldRequired),
                  ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resource.validation_BadEmailAddress),
                       ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; } = string.Empty;

        public bool Suspended { get; set; }

        public string? Rut { get; set; } = string.Empty;
    }
}
