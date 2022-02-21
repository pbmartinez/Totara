using Application.Dtos;

namespace Application.IAppServices
{
    public partial interface IPeripheralAppService : IAppService<PeripheralDto, PeripheralDtoForCreate, PeripheralDtoForUpdate>
    {
    }
}
