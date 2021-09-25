using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IAppServices
{
    public partial interface IMatriculaAppService : IAppService<MatriculaDto, MatriculaDtoForCreate, MatriculaDtoForUpdate>
    {
    }
}
