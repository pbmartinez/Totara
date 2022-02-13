using Application.Dtos;

namespace Application.IAppServices
{
    public partial interface ICasaAppService : IAppService<CasaDto, CasaDtoForCreate, CasaDtoForUpdate>
    {
    }
}
