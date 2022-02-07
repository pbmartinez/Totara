using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IAppServices
{
    public partial interface ICasaAppService : IAppService<CasaDto, CasaDtoForCreate, CasaDtoForUpdate>
    {
    }
}
