using Application.Dtos;
using System;

namespace Application.IAppServices
{
    public partial interface IUsuarioAppService : IAppService<UsuarioDto, int>
    {
    }
}
