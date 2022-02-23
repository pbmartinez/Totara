using Application.DataAnnotations;
using Domain.Entities;
using Domain.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class GatewayDtoMetadata 
    {
        [Display(ResourceType = typeof(Resource), Name = $"{nameof(GatewayDto)}{nameof(Name)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public string Name { get; set; }


        [Display(ResourceType = typeof(Resource), Name = $"{nameof(GatewayDto)}{nameof(Ipv4Address)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        [Ipv4Address(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_Ipv4AddressBadFormat")]
        public string Ipv4Address { get; set; }
    }
    [MetadataType(typeof(GatewayDtoMetadata))]
    public partial class GatewayDto : Entity, IValidatableObject 
    {
        public GatewayDto()
        {
            Peripherals = new List<PeripheralDto>();
        }

        public string Name { get; set; }


        public string Ipv4Address { get; set; }

        public List<PeripheralDto> Peripherals { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var gateway = (GatewayDto)validationContext.ObjectInstance;
            var erroResults = new List<ValidationResult>();

            if (gateway.Peripherals.Count >= Constants.GatewayPeripherals.MAX_PERIPHERALS_ALLOWED_PER_GATEWAY)
            {
                erroResults.Add(new ValidationResult(
                    errorMessage: string.Format(Resource.validation_MaxPeriphelsAllowed, Constants.GatewayPeripherals.MAX_PERIPHERALS_ALLOWED_PER_GATEWAY),
                    memberNames: new[] { nameof(Peripherals) }));
            }

            return erroResults;
        }
    }
}
