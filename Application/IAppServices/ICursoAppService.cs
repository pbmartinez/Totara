using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IAppServices
{
    public partial interface ICursoAppService : IAppService<CursoDto, CursoDtoForCreate, CursoDtoForUpdate, Domain.Entities.Entity>
    {
    }
}
