using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Domain.Repositories
{
    public class EscuelaRepository : Repository<Escuela>, IEscuelaRepository
    {
        public EscuelaRepository(IUnitOfWork unitofWork) : base(unitofWork)
        {

        }
    }
}
