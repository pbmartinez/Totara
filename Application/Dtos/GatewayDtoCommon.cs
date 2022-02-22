using Application.DataAnnotations;
using Domain.Entities;
using Domain.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class GatewayDtoCommon : Entity, IValidatableObject
    {
        private static readonly int MAX_PERIPHERALS_ALLOWED = 10;


        public GatewayDtoCommon()
        {
            Peripherals = new List<PeripheralDtoCommon>();
        }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(GatewayDtoCommon)}{nameof(Name)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public string Name { get; set; }


        [Display(ResourceType = typeof(Resource), Name = $"{nameof(GatewayDtoCommon)}{nameof(Ipv4Address)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        [Ipv4Address(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_Ipv4AddressBadFormat")]        
        public string Ipv4Address { get; set; }

        public List<PeripheralDtoCommon> Peripherals { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var gateway = (GatewayDtoCommon)validationContext.ObjectInstance;
            var erroResults = new List<ValidationResult>();


            if (gateway.Peripherals.Count >= MAX_PERIPHERALS_ALLOWED)
            {
                erroResults.Add(new ValidationResult(
                    errorMessage: string.Format(Resource.validation_MaxPeriphelsAllowed,MAX_PERIPHERALS_ALLOWED),
                    memberNames: new[] { nameof(Peripherals) }));
            }

            return erroResults;
        }
    }
}
