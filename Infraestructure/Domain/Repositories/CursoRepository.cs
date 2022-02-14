using Domain.Entities;
using Domain.IRepositories;
using Domain.UnitOfWork;

namespace Infraestructure.Domain.Repositories
{
    public partial class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(IUnitOfWork unitofWork) : base(unitofWork)
        {

        }
    }
}
