using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Domain.Repositories
{
    public class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        public PersonaRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
