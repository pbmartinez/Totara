using Application.Dtos;
using System;

namespace Application.IAppServices
{
    public partial interface IProviderAppService : IAppService<ProviderDto, Guid>
    {
    }
}
