using Application.IAppServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class PeripheralDto : PeripheralDtoCommon
    {
        public PeripheralDto(IGatewayAppService gatewayAppService) : base(gatewayAppService)
        {
        }
    }
}
