using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class EscuelaRepository : Repository<Escuela>, IEscuelaRepository
    {
        public EscuelaRepository(IUnitOfWork unitofWork) : base(unitofWork)
        {

        }
    }
}
