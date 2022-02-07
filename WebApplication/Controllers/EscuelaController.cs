using Application.Dtos;
using Application.IAppServices;

namespace WebApplication.Controllers
{
    public class EscuelaController : BaseController<EscuelaDto, EscuelaDtoForCreate, EscuelaDtoForUpdate>
    {
        public EscuelaController(IEscuelaAppService EscuelaAppService) : base(EscuelaAppService)
        {

        }
    }
}
