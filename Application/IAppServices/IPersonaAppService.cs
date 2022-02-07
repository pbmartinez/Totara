using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IAppServices
{
    public partial interface IPersonaAppService : IAppService<PersonaDto, PersonaDtoForCreate, PersonaDtoForUpdate, Domain.Entities.Entity>
    {

    }
}
