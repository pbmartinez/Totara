using Application.Dtos;
using Application.IAppServices;

namespace WebApplication.Controllers
{
    public class PersonaController : BaseController<PersonaDto, PersonaDtoForCreate, PersonaDtoForUpdate>
    {
        public PersonaController(IPersonaAppService PersonaAppService) : base(PersonaAppService)
        {

        }
    }
}
