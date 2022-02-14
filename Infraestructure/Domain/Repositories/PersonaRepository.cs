using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        public PersonaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
