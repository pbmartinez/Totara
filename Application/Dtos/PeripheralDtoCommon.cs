using Application.IAppServices;
using Domain.Entities;
using Domain.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class PeripheralDtoCommon : Entity, IValidatableObject
    {
        private static readonly int MAX_PERIPHERALS_ALLOWED = 10;
        private readonly IGatewayAppService _gatewayAppService;

        public PeripheralDtoCommon()
        {

        }
        public PeripheralDtoCommon(IGatewayAppService gatewayAppService)
        {
            _gatewayAppService = gatewayAppService ?? throw new ArgumentNullException(nameof(gatewayAppService));
        }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDtoCommon)}{nameof(Vendor)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public string Vendor { get; set; }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDtoCommon)}{nameof(CreatedDate)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public DateTime CreatedDate { get; set; }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDtoCommon)}{nameof(Status)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public bool Status { get; set; }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDtoCommon)}{nameof(GatewayId)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public Guid GatewayId { get; set; }

        public GatewayDtoCommon Gateway { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var peripheral = (PeripheralDtoCommon)validationContext.ObjectInstance;
            var erroResults = new List<ValidationResult>();

            var gateway = _gatewayAppService.Get(peripheral.GatewayId);

            if (gateway.Peripherals.Count >= MAX_PERIPHERALS_ALLOWED)
            {
                erroResults.Add(new ValidationResult(
                    errorMessage: string.Format(Resource.validation_MaxPeriphelsAllowed, MAX_PERIPHERALS_ALLOWED),
                    memberNames: new[] { nameof(GatewayId) }));
            }

            return erroResults;
        }

    }
}
