using Application.Dtos;

namespace Application.IAppServices
{
    public interface ICategoriaAppService : IAppService<CategoriaDto, CategoriaDtoForCreate, CategoriaDtoForUpdate>
    {
    }
}
