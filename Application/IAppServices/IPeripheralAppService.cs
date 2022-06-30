using Application.Dtos;
using System;

namespace Application.IAppServices
{
    public partial interface IPeripheralAppService : IAppService<PeripheralDto, Guid>
    {
    }
}
