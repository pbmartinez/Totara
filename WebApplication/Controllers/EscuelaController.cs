using Application.Dtos;
using Application.IAppServices;

namespace WebApplication.Controllers
{
    public class EscuelaController : BaseController<EscuelaDto, EscuelaDtoForCreate, EscuelaDtoForUpdate>
    {
        public EscuelaController(IEscuelaAppService EscuelaAppService) : base(EscuelaAppService)
        {
            Includes = new() { a => a.Cursos, a => a.Estudiantes };
            DetailsIncludes = new() { a => a.Cursos, a => a.Estudiantes };
            DeleteIncludes = new() { a => a.Cursos, a => a.Estudiantes };
            EditIncludes = new() { a => a.Cursos, a => a.Estudiantes };
            
        }
    }
}
