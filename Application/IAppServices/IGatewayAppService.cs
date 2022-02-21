using Application.Dtos;

namespace Application.IAppServices
{
    public partial interface IGatewayAppService : IAppService<GatewayDto, GatewayDtoForCreate, GatewayDtoForUpdate>
    {
    }
}
