using Application.IAppServices;
using Domain.Entities;
using Domain.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public partial class PeripheralDto : Entity, IValidatableObject
    {
        public IGatewayAppService GatewayAppService { get; set; }
        public PeripheralDto()
        {
            CreatedDate = DateTime.Now;
            CreatedDateHelper = DateTime.Now;
        }
        public PeripheralDto(IGatewayAppService gatewayAppService) 
        {
            GatewayAppService = gatewayAppService ?? throw new ArgumentNullException(nameof(gatewayAppService));
            CreatedDate = DateTime.Now;
            CreatedDateHelper = DateTime.Now;
        }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDto)}{nameof(Vendor)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public string Vendor { get; set; }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDto)}{nameof(CreatedDate)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public DateTime CreatedDate { get; set; }
        
        public DateTime? CreatedDateHelper { get; set; }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDto)}{nameof(Status)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public bool Status { get; set; }

        [Display(ResourceType = typeof(Resource), Name = $"{nameof(PeripheralDto)}{nameof(GatewayId)}")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "validation_FieldRequired")]
        public Guid GatewayId { get; set; }

        public GatewayDto Gateway { get; set; }
        public ProviderDto Provider { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var peripheral = (PeripheralDto)validationContext.ObjectInstance;
            var erroResults = new List<ValidationResult>();
            
            //TODO fix call validation from fronted app: 1. endpoint for validation :). 2. import infrastructure in fronten app :(

            //var gateway = GatewayAppService.Get(peripheral.GatewayId);

            //if (gateway.Peripherals.Count >= Constants.GatewayPeripherals.MAX_PERIPHERALS_ALLOWED_PER_GATEWAY)
            //{
            //    erroResults.Add(new ValidationResult(
            //        errorMessage: string.Format(Resource.validation_MaxPeriphelsAllowed, Constants.GatewayPeripherals.MAX_PERIPHERALS_ALLOWED_PER_GATEWAY),
            //        memberNames: new[] { nameof(GatewayId) }));
            //}

            return erroResults;
        }
    }
}
