using Application.Dtos;
using System;

namespace Application.IAppServices
{
    public partial interface IBrandAppService : IAppService<BrandDto, Guid>
    {
    }
}
