using Application.Dtos;
using System;

namespace Application.IAppServices
{
    public partial interface IGatewayAppService : IAppService<GatewayDto, Guid>
    {
    }
}
