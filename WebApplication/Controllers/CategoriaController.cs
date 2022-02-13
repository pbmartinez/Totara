using Application.Dtos;
using Application.IAppServices;

namespace WebApplication.Controllers
{
    public class CategoriaController : BaseController<CategoriaDto, CategoriaDtoForCreate, CategoriaDtoForUpdate>
    {
        public CategoriaController(ICategoriaAppService appService) : base(appService)
        {
            DefaultOrderBy = new() { { "Nombre", true } };
        }
    }
}
