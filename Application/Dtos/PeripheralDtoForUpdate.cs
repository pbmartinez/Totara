using Application.IAppServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public partial class PeripheralDtoForUpdate : PeripheralDtoCommon
    {
        public PeripheralDtoForUpdate(IGatewayAppService gatewayAppService) : base(gatewayAppService)
        {
        }
    }
}
